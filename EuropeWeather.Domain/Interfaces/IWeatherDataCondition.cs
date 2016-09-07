using EuropeWeather.Domain.Models;

namespace EuropeWeather.Domain.Interfaces
{
    public interface IWeatherDataCondition
    {
        long Id { get; set; }
        WeatherDataDto WeatherData { get; set; }
        WeatherConditionDto WeatherCondition { get; set; }
        string Main { get; set; }
        string Icon { get; set; }
    }
}