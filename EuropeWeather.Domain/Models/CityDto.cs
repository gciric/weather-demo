using System.Collections.Generic;
using EuropeWeather.Domain.Interfaces;
using EuropeWeather.Domain.Models.Base;

namespace EuropeWeather.Domain.Models
{
    public class CityDto : Lookup, ICity
    {
        public CountryDto Country { get; set; }
        public int? ExternalId { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public IEnumerable<IWeatherData> Weather { get; set; }

    }
}