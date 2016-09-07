using EuropeWeather.Domain.Interfaces;
using EuropeWeather.Domain.Models.Base;

namespace EuropeWeather.Domain.Models
{
    public class WeatherConditionDto : Lookup, IWeatherCondition
    {
        public int ExternalId { get; set; }
    }
}