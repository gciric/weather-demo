using System.Collections.Generic;
using EuropeWeather.Domain.Interfaces.Base;
using EuropeWeather.Domain.Models;

namespace EuropeWeather.Domain.Interfaces
{
    public interface ICity : ILookup
    {
        CountryDto Country { get; set; }
        int? ExternalId { get; set; }
        decimal? Longitude { get; set; }
        decimal? Latitude { get; set; }
        IEnumerable<IWeatherData> Weather { get; set; }
    }
}