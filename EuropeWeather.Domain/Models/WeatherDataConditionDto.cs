using EuropeWeather.Domain.Interfaces;

namespace EuropeWeather.Domain.Models
{
    public class WeatherDataConditionDto : IWeatherDataCondition
    {
        public long Id { get; set; }
        public WeatherDataDto WeatherData { get; set; }
        public WeatherConditionDto WeatherCondition { get; set; }
        public string Main { get; set; }
        public string Icon { get; set; }
    }
}