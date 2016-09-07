using System;
using System.Collections.Generic;
using EuropeWeather.Domain.Interfaces;

namespace EuropeWeather.Domain.Models
{
    public class WeatherDataDto : IWeatherData
    {
        public long Id { get; set; }
        public CityDto City { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? Pressure { get; set; }
        public decimal? Humidity { get; set; }
        public decimal? MinTemperature { get; set; }
        public decimal? MaxTemperature { get; set; }
        public int? Visibility { get; set; }
        public decimal? WindSpeed { get; set; }
        public decimal? WindDirection { get; set; }
        public decimal? Clouds { get; set; }
        public decimal? Rain { get; set; }
        public decimal? Snow { get; set; }
        public DateTime? Sunrise { get; set; }
        public DateTime? Sunset { get; set; }
        public DateTime? TimeOfCalculation { get; set; }
        public DateTime? Created { get; set; }
        public IEnumerable<WeatherDataConditionDto> Conditions { get; set; }
    }
}