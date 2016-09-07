using EuropeWeather.Domain.Interfaces.Base;

namespace EuropeWeather.Domain.Models.Base
{
    public abstract class Lookup : ILookup
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
