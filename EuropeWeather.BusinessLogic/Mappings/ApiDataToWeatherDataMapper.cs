using System;
using System.Collections.Generic;
using System.Linq;
using EuropeWeather.BusinessLogic.Common;
using EuropeWeather.BusinessLogic.Interfaces;
using EuropeWeather.DataAccess;
using EuropeWeather.Integration.OpenWeatherMap.Models;

namespace EuropeWeather.BusinessLogic.Mappings
{
    public class ApiDataToWeatherDataMapper : IMapping<ApiData, IEnumerable<WeatherDataConditions>>
    {
        private readonly EuropeWeatherEntities _context;

        public ApiDataToWeatherDataMapper(EuropeWeatherEntities context)
        {
            _context = context;
        }

        public IEnumerable<WeatherDataConditions> To(ApiData source)
        {
            var city = GetCityByExternalId(source.Id);
            var weather = source.Weather?.ToArray();

            if (city == null || weather == null || weather.Length == 0) return null;

            var weatherData = _context.WeatherData.Create();
            weatherData.Cities = city;
            weatherData.Temperature = source.Main.Temp;
            weatherData.Pressure = source.Main.Pressure;
            weatherData.Humidity = source.Main.Humidity;
            weatherData.MinTemperature = source.Main.TempMin;
            weatherData.MaxTemperature = source.Main.TempMax;
            weatherData.Visibility = source.Visibility;
            weatherData.WindSpeed = source.Wind?.Speed;
            weatherData.WindDirection = source.Wind?.Deg;
            weatherData.Clouds = source.Clouds?.All;
            weatherData.Rain = source.Rain?._3H;
            weatherData.Snow = source.Snow?._3H;
            weatherData.Sunrise =
                source.Sys.Sunrise.HasValue
                    ? Converter.UnixTimeStampToDateTime(source.Sys.Sunrise.Value)
                    : (DateTime?) null;
            weatherData.Sunset =
                source.Sys.Sunset.HasValue
                    ? Converter.UnixTimeStampToDateTime(source.Sys.Sunset.Value)
                    : (DateTime?) null;
            weatherData.TimeOfCalculation = Converter.UnixTimeStampToDateTime(source.Dt);
                weatherData.Created = DateTime.UtcNow;

            var mapper = new ApiWeatherDataToWeatherDataConditionsMapper(_context, weatherData);
            return weather.Select(w => mapper.To(w)).ToList();
        }

        private Cities GetCityByExternalId(int externalId)
        {
            return _context.Cities.FirstOrDefault(c => c.ExternalId == externalId);
        }

        

        public ApiData From(IEnumerable<WeatherDataConditions> destination)
        {
            throw new NotImplementedException();
        }
    }

    public class ApiWeatherDataToWeatherDataConditionsMapper : IMapping<Weather, WeatherDataConditions>
    {
        private readonly EuropeWeatherEntities _context;
        private readonly WeatherData _weatherData;

        public ApiWeatherDataToWeatherDataConditionsMapper(EuropeWeatherEntities context, WeatherData weatherData)
        {
            _context = context;
            _weatherData = weatherData;
        }

        private WeatherConditions GetWeatherConditionByExternalId(int externalId)
        {
            return _context.WeatherConditions.FirstOrDefault(c => c.ExternalId == externalId);
        }

        public WeatherDataConditions To(Weather source)
        {
            if (!source.Id.HasValue) return null;

            var data = _context.WeatherDataConditions.Create();
            data.WeatherData = _weatherData;
            data.WeatherConditions = GetWeatherConditionByExternalId(source.Id.Value);
            data.WeatherIcon = source.Icon;
            data.WeatherMain = source.Main;

            return data;
        }

        public Weather From(WeatherDataConditions destination)
        {
            throw new NotImplementedException();
        }

    }
}
