using System;
using System.Collections.Generic;
using EuropeWeather.Domain.Models;

namespace EuropeWeather.Domain.Interfaces
{
    public interface IWeatherData
    {
        long Id { get; set; }
        CityDto City { get; set; }
        decimal? Temperature { get; set; }
        decimal? Pressure { get; set; }
        decimal? Humidity { get; set; }
        decimal? MinTemperature { get; set; }
        decimal? MaxTemperature { get; set; }
        int? Visibility { get; set; }
        decimal? WindSpeed { get; set; }
        decimal? WindDirection { get; set; }
        decimal? Clouds { get; set; }
        decimal? Rain { get; set; }
        decimal? Snow { get; set; }
        DateTime? Sunrise { get; set; }
        DateTime? Sunset { get; set; }
        DateTime? TimeOfCalculation { get; set; }
        DateTime? Created { get; set; }
        IEnumerable<WeatherDataConditionDto> Conditions { get; set; }

    }
}