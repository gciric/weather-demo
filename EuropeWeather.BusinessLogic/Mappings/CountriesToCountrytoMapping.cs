using EuropeWeather.BusinessLogic.Interfaces;
using EuropeWeather.DataAccess;
using EuropeWeather.Domain.Interfaces;
using EuropeWeather.Domain.Models;

namespace EuropeWeather.BusinessLogic.Mappings
{
    public class CountriesToCountrytoMapping : IMapping<Countries, CountryDto>
    {
        private readonly EuropeWeatherEntities _context;
        public CountriesToCountrytoMapping(EuropeWeatherEntities context)
        {
            _context = context;
        }

        public CountryDto To(Countries source)
        {
            return new CountryDto
            {
                Id = source.CountryId,
                Name = source.Name,
                Code2 = source.Code2,
                Code3 = source.Code3
            };
        }

        public Countries From(CountryDto destination)
        {
            var country = destination.Id > 0 ? _context.Countries.Find(destination.Id) : _context.Countries.Create();
            country.Name = destination.Name;
            country.Code2 = destination.Code2;
            country.Code3 = destination.Code3;

            return country;
        }


        public Countries FromId(int countryId)
        {
            var country =  _context.Countries.Find(countryId);
            return country;
        }
    }
}