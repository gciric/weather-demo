using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EuropeWeather.BusinessLogic.Interfaces;
using EuropeWeather.BusinessLogic.Mappings;
using EuropeWeather.DataAccess;
using EuropeWeather.Domain.Interfaces;
using EuropeWeather.Domain.Models;
using EuropeWeather.Integration.OpenWeatherMap;
using System.Configuration;

namespace EuropeWeather.BusinessLogic.Services
{
    public class WeatherDataService : IWeatherDataService
    {
        private const int MAX_RESULTS = 100;

        public async Task<bool> ImportWeatherData(string url, string apiKey)
        {
            int[] ids;
            var result = false;
            using (var context = new EuropeWeatherEntities())
            {
                ids = await context.Cities.Where(c => c.ExternalId.HasValue).Select(c => c.ExternalId.Value).ToArrayAsync();
            }

            var api = new CurrentWeather();
            var apiData = api.GetCurrentWeather(url, ids, apiKey);
            if (apiData == null || apiData.Count == 0) return true; //no results but no error

            using (var context = new EuropeWeatherEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var mapper = new ApiDataToWeatherDataMapper(context);
                        foreach (var conditions in apiData.List.Select(weatherData => mapper.To(weatherData)))
                        {
                            context.WeatherDataConditions.AddRange(conditions);
                        }
                        context.SaveChanges();
                        transaction.Commit();
                        result = true;
                    }
                    catch (Exception)
                    {
                        //TODO: Log error
                        transaction.Rollback();
                    }
                }
            }

            return result;
        }

        public async Task<bool> ImportWeatherData(string url, string apiKey, int cityId)
        {
            var result = false;
            int? externalId;
            using (var context = new EuropeWeatherEntities())
            {
                externalId =
                    await
                        context.Cities.Where(c => c.ExternalId.HasValue && c.CityId == cityId)
                            .Select(c => c.ExternalId)
                            .FirstOrDefaultAsync();
            }

            if (!externalId.HasValue) return false;

            var api = new CurrentWeather();

            var apiData = api.GetCurrentWeather(url, externalId.Value, apiKey);
            if (apiData == null) return true; //no data received and no error

            using (var context = new EuropeWeatherEntities())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var mapper = new ApiDataToWeatherDataMapper(context);
                        var weatherData = mapper.To(apiData);
                        context.WeatherDataConditions.AddRange(weatherData);
                        context.SaveChanges();
                        transaction.Commit();
                        result = true;

                    }
                    catch (Exception)
                    {
                        //TODO: Log error
                        transaction.Rollback();
                    }
                }
            }

            return result;
        }

        public async Task<IEnumerable<ICity>> GetCities()
        {
            using (var context = new EuropeWeatherEntities())
            {
                var cities = await context.Cities.Include(c => c.Countries).OrderBy(c => c.Countries.Name).ToListAsync();
                var mapper = new CitiesToCityDtoMapping(context);
                return !cities.Any() ? new List<CityDto>() : cities.Select(c => mapper.To(c)).ToList();
            }
        }

        public async Task<ICity> GetCity(int cityId)
        {
            ICity cityDto;
            using (var context = new EuropeWeatherEntities())
            {
                var city = await context.Cities.Include(c => c.Countries).FirstOrDefaultAsync(c => c.CityId == cityId);
                if (city == null) return null;
                var mapper = new CitiesToCityDtoMapping(context);
                cityDto = mapper.To(city);
            }
            return cityDto;
        }

        public async Task<IEnumerable<IWeatherData>> GetWeatherDataForDate(DateTime date)
        {
            using (var context = new EuropeWeatherEntities())
            {
                var weatherData =
                    await
                        context.WeatherData.Include(wde => wde.Cities)
                            .Where(
                                w =>
                                    w.TimeOfCalculation.HasValue && w.TimeOfCalculation.Value.Year == date.Year &&
                                    w.TimeOfCalculation.Value.Month == date.Month &&
                                    w.TimeOfCalculation.Value.Day == date.Day)
                            .OrderBy(w => w.Cities.Countries.Name)
                            .ToListAsync();
                var mapper = new WeatherDataToWeatherDataDtoMapping(context);
                return weatherData.Select(w => mapper.To(w)).ToList();
            }
        }

        public async Task<Tuple<IEnumerable<IWeatherData>,int>> SearchWeatherData(IEnumerable<int> cities = null,
            TimeSpan? sunsetFrom = null, TimeSpan? sunsetTo = null, TimeSpan? sunriseFrom = null,
            TimeSpan? sunriseTo = null, DateTime? from = null, DateTime? to = null, int? minTemp = null,
            int? maxTemp = null)
        {
            using (var context = new EuropeWeatherEntities())
            {
                var filter = context.WeatherData.Include(wde => wde.Cities);
                var cityFilter = cities?.ToArray();
                if (cityFilter != null && cityFilter.Any())
                {
                    filter = filter.Where(w => cityFilter.Contains(w.CityId));
                }
                if (sunsetFrom.HasValue)
                {
                    filter =
                        filter.Where(
                            w =>
                                w.Sunset.HasValue && w.Sunset.Value.Hour >= sunsetFrom.Value.Hours &&
                                w.Sunset.Value.Minute >= sunsetFrom.Value.Minutes);
                }
                if (sunsetTo.HasValue)
                {
                    filter =
                        filter.Where(
                            w =>
                                w.Sunset.HasValue && w.Sunset.Value.Hour <= sunsetTo.Value.Hours &&
                                w.Sunset.Value.Minute <= sunsetTo.Value.Minutes);
                }
                if (sunriseFrom.HasValue)
                {
                    filter =
                        filter.Where(
                            w =>
                                w.Sunset.HasValue && w.Sunset.Value.Hour >= sunriseFrom.Value.Hours &&
                                w.Sunset.Value.Minute >= sunriseFrom.Value.Minutes);
                }
                if (sunriseTo.HasValue)
                {
                    filter =
                        filter.Where(
                            w =>
                                w.Sunset.HasValue && w.Sunset.Value.Hour <= sunriseTo.Value.Hours &&
                                w.Sunset.Value.Minute <= sunriseTo.Value.Minutes);
                }
                if (from.HasValue)
                {
                    filter = filter.Where(
                        w =>
                            w.TimeOfCalculation.HasValue && w.TimeOfCalculation.Value.Year >= from.Value.Year &&
                            w.TimeOfCalculation.Value.Month >= from.Value.Month &&
                            w.TimeOfCalculation.Value.Day >= from.Value.Day);
                }
                if (to.HasValue)
                {
                    filter = filter.Where(
                        w =>
                            w.TimeOfCalculation.HasValue && w.TimeOfCalculation.Value.Year <= to.Value.Year &&
                            w.TimeOfCalculation.Value.Month <= to.Value.Month &&
                            w.TimeOfCalculation.Value.Day <= to.Value.Day);
                }
                if (minTemp.HasValue)
                {
                    filter = filter.Where(w => w.Temperature.HasValue && w.Temperature.Value >= minTemp);
                }
                if (maxTemp.HasValue)
                {
                    filter = filter.Where(w => w.Temperature.HasValue && w.Temperature.Value <= maxTemp);
                }

               var count = filter.Count();

                var weatherData =
                    await
                        filter.OrderBy(w => w.Cities.Countries.Name)
                            .ThenByDescending(w => w.Created)
                            .Take(MAX_RESULTS)
                            .ToListAsync();
                var mapper = new WeatherDataToWeatherDataDtoMapping(context);
                return new Tuple<IEnumerable<IWeatherData>, int>(weatherData.Select(w => mapper.To(w)).ToList(), count);
            }

        }

        public async Task<Tuple<IEnumerable<IWeatherData>, int>> GetLatestWeatherData(IEnumerable<int> cities = null,
            TimeSpan? sunsetFrom = null, TimeSpan? sunsetTo = null, TimeSpan? sunriseFrom = null,
            TimeSpan? sunriseTo = null, DateTime? from = null, DateTime? to = null, int? minTemp = null,
            int? maxTemp = null)
        {
            var url = ConfigurationManager.AppSettings["api:url"];
            var apiKey = ConfigurationManager.AppSettings["api:key"];
            int refreshInterval;
            if (!int.TryParse(ConfigurationManager.AppSettings["api:interval"], out refreshInterval))
            {
                refreshInterval = 10;
            }
            using (var context = new EuropeWeatherEntities())
            {
                var latest = context.WeatherData.Max(w => w.Created);
                if (!latest.HasValue || (DateTime.UtcNow - latest.Value).TotalMinutes > refreshInterval)
                {
                    var sync = await ImportWeatherData(url, apiKey);
                    if (!sync)
                    {
                        //TODO: Log unsuccessfull sync
                    }
                }

                var filter = context.WeatherData.Include(w => w.Cities)
                    .GroupBy(w => w.CityId, w => w,
                        (key, g) =>
                            new
                            {
                                CityId = key,
                                WeatherData = g.OrderByDescending(w => w.Created).FirstOrDefault()
                            })
                    .Select(r => r.WeatherData);
                var cityFilter = cities?.ToArray();
                if (cityFilter != null && cityFilter.Any())
                {
                    filter = filter.Where(w => cityFilter.Contains(w.CityId));
                }
                if (sunsetFrom.HasValue)
                {
                    filter =
                        filter.Where(
                            w =>
                                w.Sunset.HasValue && w.Sunset.Value.Hour >= sunsetFrom.Value.Hours &&
                                w.Sunset.Value.Minute >= sunsetFrom.Value.Minutes);
                }
                if (sunsetTo.HasValue)
                {
                    filter =
                        filter.Where(
                            w =>
                                w.Sunset.HasValue && w.Sunset.Value.Hour <= sunsetTo.Value.Hours &&
                                w.Sunset.Value.Minute <= sunsetTo.Value.Minutes);
                }
                if (sunriseFrom.HasValue)
                {
                    filter =
                        filter.Where(
                            w =>
                                w.Sunset.HasValue && w.Sunset.Value.Hour >= sunriseFrom.Value.Hours &&
                                w.Sunset.Value.Minute >= sunriseFrom.Value.Minutes);
                }
                if (sunriseTo.HasValue)
                {
                    filter =
                        filter.Where(
                            w =>
                                w.Sunset.HasValue && w.Sunset.Value.Hour <= sunriseTo.Value.Hours &&
                                w.Sunset.Value.Minute <= sunriseTo.Value.Minutes);
                }
                if (from.HasValue)
                {
                    filter = filter.Where(
                        w =>
                            w.TimeOfCalculation.HasValue && w.TimeOfCalculation.Value.Year >= from.Value.Year &&
                            w.TimeOfCalculation.Value.Month >= from.Value.Month &&
                            w.TimeOfCalculation.Value.Day >= from.Value.Day);
                }
                if (to.HasValue)
                {
                    filter = filter.Where(
                        w =>
                            w.TimeOfCalculation.HasValue && w.TimeOfCalculation.Value.Year <= to.Value.Year &&
                            w.TimeOfCalculation.Value.Month <= to.Value.Month &&
                            w.TimeOfCalculation.Value.Day <= to.Value.Day);
                }
                if (minTemp.HasValue)
                {
                    filter = filter.Where(w => w.Temperature.HasValue && w.Temperature.Value >= minTemp);
                }
                if (maxTemp.HasValue)
                {
                    filter = filter.Where(w => w.Temperature.HasValue && w.Temperature.Value <= maxTemp);
                }

                var count = filter.Count();
                var weatherData = await filter.OrderBy(w => w.Cities.Countries.Name).Take(MAX_RESULTS).ToListAsync();

                var mapper = new WeatherDataToWeatherDataDtoMapping(context);
                return new Tuple<IEnumerable<IWeatherData>, int>(weatherData.Select(w => mapper.To(w)).ToList(), count);
            }
        }

    }
}
