using System;
using System.Collections.Generic;
using EuropeWeather.Domain.Interfaces;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using EuropeWeather.BusinessLogic.Common;

namespace EuropeWeather.Web.Models.WeatherData
{
    public class WeatherDataModel
    {
        public WeatherDataModel()
        {
            ShowLatestData = true;
        }

        public IEnumerable<int> SelectedCities { get; set; }
        [DisplayName("Cities")]
        public IEnumerable<ICity> Cities { get; set; }
        [DisplayName("Date From")]
        public DateTime? From { get; set; }
        [DisplayName("Date To")]
        public DateTime? To { get; set; }
        [DisplayName("Max. Temperature")]
        public int? MaxTemperature { get; set; }
        [DisplayName("Min. Temperature")]
        public int? MinTemperature { get; set; }
        [DisplayName("Sunrise From")]
        public TimeSpan? SunriseFrom { get; set; }
        [DisplayName("Sunrise To")]
        public TimeSpan? SunriseTo { get; set; }
        [DisplayName("Sunset From")]
        public TimeSpan? SunsetFrom { get; set; }
        [DisplayName("Sunset To")]
        public TimeSpan? SunsetTo { get; set; }
        [DisplayName("Show only lastes weather data")]
        public bool ShowLatestData { get; set; }

        public IEnumerable<IWeatherData> Results { get; set; }

        [DisplayName("Units")]
        public Measurements SelectedUnit { get; set; }

        public static Dictionary<Measurements, string> TemperatureUnits = new Dictionary<Measurements, string>
        {
            {Measurements.Default, "K"},
            {Measurements.Metric, "C"},
            {Measurements.Imperial, "F"}
        };

        public static Dictionary<Measurements, string> SpeedUnits = new Dictionary<Measurements, string>
        {
            {Measurements.Default, "meter/sec"},
            {Measurements.Metric, "meter/sec"},
            {Measurements.Imperial, "miles/hour"}
        };


        public static IEnumerable<SelectListItem> Units = new List<SelectListItem>
        {
            new SelectListItem {Text = "Default", Value = ((int) Measurements.Default).ToString()},
            new SelectListItem {Text = "Metric", Value = ((int) Measurements.Metric).ToString()},
            new SelectListItem {Text = "Imperial", Value = ((int) Measurements.Imperial).ToString()}
        };

        public int? MinTemperatureInK()
        {
            if (!MinTemperature.HasValue) return null;
            switch (SelectedUnit)
            {
                case Measurements.Metric:
                    return (int)Converter.FromCelsiusToKelvin(MinTemperature.Value);
                case Measurements.Imperial:
                    return (int)Converter.FromFahrenheitToKelvin(MinTemperature.Value);
                default:
                   return MinTemperature;
            }
        }

        public int? MaxTemperatureInK()
        {
            if (!MaxTemperature.HasValue) return null;
            switch (SelectedUnit)
            {
                case Measurements.Metric:
                    return (int)Converter.FromCelsiusToKelvin(MaxTemperature.Value);
                case Measurements.Imperial:
                    return (int)Converter.FromFahrenheitToKelvin(MaxTemperature.Value);
                default:
                    return MaxTemperature;
            }
        }

        public IEnumerable<SelectListItem> GetCities()
        {
            var list = Cities?.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Name} ({c.Country.Code2})",
                Selected = SelectedCities != null && SelectedCities.Contains(c.Id)}).ToList() ?? new List<SelectListItem>();

            if (list.Any())
            {
                list.Insert(0, new SelectListItem {Value = "0", Text = "All"});
            }

            return list;
        }
    }

    public enum Measurements
    {
        Default,
        Metric,
        Imperial
    }
}