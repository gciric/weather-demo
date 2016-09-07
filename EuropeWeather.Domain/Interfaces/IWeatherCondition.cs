using EuropeWeather.Domain.Interfaces.Base;

namespace EuropeWeather.Domain.Interfaces
{
    public interface IWeatherCondition : ILookup
    {
        int ExternalId { get; set; }
    }
}