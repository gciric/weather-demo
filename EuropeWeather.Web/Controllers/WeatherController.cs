using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EuropeWeather.BusinessLogic.Common;
using EuropeWeather.BusinessLogic.Services;
using EuropeWeather.Domain.Interfaces;
using EuropeWeather.Web.Models;
using EuropeWeather.Web.Models.WeatherData;

namespace EuropeWeather.Web.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherDataService _dataService = new WeatherDataService();

        // GET: Weather
        public async Task<ActionResult> Index(WeatherDataModel model)
        {
            if (model.Cities == null)
            {
                if (Session["cities"] == null)
                {
                    model.Cities = await _dataService.GetCities();
                    Session.Add("cities", model.Cities);
                }
                else
                {
                    model.Cities = (IEnumerable<ICity>)Session["cities"];
                }
            }

            if (Session["unit"] != null)
            {
                var sessionUnit = (Measurements) Session["unit"];
                if (Request.HttpMethod == "POST")
                {
                    Session["unit"] = model.SelectedUnit;
                }
                else
                {
                    model.SelectedUnit = sessionUnit;
                }
            }
            else
            {
                if (model.SelectedUnit != Measurements.Default) Session.Add("unit", model.SelectedUnit);
            }

            Tuple<IEnumerable<IWeatherData>, int> tuple;
            IEnumerable<IWeatherData> weatherData;
            if (model.SelectedCities?.Any() ?? false)
            {
                model.SelectedCities = model.SelectedCities.Where(i => i > 0).ToArray();
            }
            if (model.ShowLatestData)
            {
                tuple =
                    await
                        _dataService.GetLatestWeatherData(model.SelectedCities, model.SunsetFrom, model.SunsetTo,
                            model.SunriseFrom, model.SunriseTo, model.From, model.To, model.MinTemperatureInK(),
                            model.MaxTemperatureInK());
            }
            else
            {
                tuple =
                    await
                        _dataService.SearchWeatherData(model.SelectedCities, model.SunsetFrom, model.SunsetTo,
                            model.SunriseFrom, model.SunriseTo, model.From, model.To, model.MinTemperatureInK(),
                            model.MaxTemperatureInK());
            }
            weatherData = tuple.Item1;
            model.TotalResults = tuple.Item2;

            model.Results = weatherData.ToList();

            if (model.SelectedUnit != Measurements.Default)
            {
                foreach (var data in model.Results)
                {
                    switch (model.SelectedUnit)
                    {
                        case Measurements.Metric:
                            data.Temperature = data.Temperature.HasValue ? Converter.FromKelvinToCelsius(data.Temperature.Value) : data.Temperature;
                            data.MinTemperature = data.MinTemperature.HasValue ? Converter.FromKelvinToCelsius(data.MinTemperature.Value) : data.MinTemperature;
                            data.MaxTemperature = data.MaxTemperature.HasValue ? Converter.FromKelvinToCelsius(data.MaxTemperature.Value) : data.MaxTemperature;
                            break;
                        case Measurements.Imperial:
                            data.Temperature = data.Temperature.HasValue ? Converter.FromKelvinToFahrenheit(data.Temperature.Value) : data.Temperature;
                            data.MinTemperature = data.MinTemperature.HasValue ? Converter.FromKelvinToFahrenheit(data.MinTemperature.Value) : data.MinTemperature;
                            data.MaxTemperature = data.MaxTemperature.HasValue ? Converter.FromKelvinToFahrenheit(data.MaxTemperature.Value) : data.MaxTemperature;
                            data.WindSpeed = data.WindSpeed.HasValue ? Converter.MetersPerSecondToMilesPerHour(data.WindSpeed.Value) : data.WindSpeed;
                            break;
                    }
                }
            }


            return View(model);
        }

        public async Task<ActionResult> SyncWeather()
        {
            var service = new WeatherDataService();
            var url = ConfigurationManager.AppSettings["api:url"];
            var apiKey = ConfigurationManager.AppSettings["api:key"];
            var model = new WeatherSyncModel();
            try
            {
                var result = await service.ImportWeatherData(url, apiKey);
                if (result)
                {
                    model.Success = true;
                    model.Message = "Sync done";
                }
                else
                {
                    model.Success = false;
                    model.Message = "API returned false";
                }
            }
            catch (Exception ex)
            {
                model.Success = false;
                model.Message = ex.Message;
            }

            return View(model);
        }
    }
}
