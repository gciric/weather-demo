using EuropeWeather.Domain.Interfaces.Base;

namespace EuropeWeather.Domain.Interfaces
{
    public interface ICountry : ILookup
    {
        string Code2 { get; set; }
        string Code3 { get; set; }
    }
}
