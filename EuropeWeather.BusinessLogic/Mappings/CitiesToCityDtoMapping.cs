using EuropeWeather.BusinessLogic.Interfaces;
using EuropeWeather.DataAccess;
using EuropeWeather.Domain.Interfaces;
using EuropeWeather.Domain.Models;

namespace EuropeWeather.BusinessLogic.Mappings
{
    public class CitiesToCityDtoMapping : IMapping<Cities, CityDto>
    {
        private readonly EuropeWeatherEntities _context;
        public CitiesToCityDtoMapping(EuropeWeatherEntities context)
        {
            _context = context;
        }

        public CityDto To(Cities source)
        {
            var mapper = new CountriesToCountrytoMapping(_context);
            var country = mapper.To(source.Countries);
            return new CityDto
            {
                Id = source.CityId,
                Name = source.CityName,
                Country = country,
                ExternalId = source.ExternalId,
                Longitude = source.Longitude,
                Latitude = source.Latitude
            };
        }

        public Cities From(CityDto destination)
        {
            var mapper = new CountriesToCountrytoMapping(_context);
            var country = mapper.FromId(destination.Country.Id);
            var city = destination.Id > 0 ? _context.Cities.Find(destination.Id) : _context.Cities.Create();
            city.CountryId = country.CountryId;
            city.ExternalId = destination.ExternalId;
            city.Longitude = destination.Longitude;
            city.Latitude = destination.Latitude;

            return city;
        }

        public Cities FromId(int cityId)
        {
            var city = _context.Cities.Find(cityId);
            return city;
        }
    }
}