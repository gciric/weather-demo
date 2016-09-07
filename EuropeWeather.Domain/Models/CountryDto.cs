using EuropeWeather.Domain.Interfaces;
using EuropeWeather.Domain.Models.Base;

namespace EuropeWeather.Domain.Models
{
    public class CountryDto : Lookup, ICountry
    {
        public string Code2 { get; set; }
        public string Code3 { get; set; }
    }
}
