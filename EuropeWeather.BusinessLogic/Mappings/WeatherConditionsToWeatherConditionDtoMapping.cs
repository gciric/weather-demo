using System.Linq;
using EuropeWeather.BusinessLogic.Interfaces;
using EuropeWeather.DataAccess;
using EuropeWeather.Domain.Interfaces;
using EuropeWeather.Domain.Models;

namespace EuropeWeather.BusinessLogic.Mappings
{
    public class WeatherConditionsToWeatherConditionDtoMapping : IMapping<WeatherConditions, WeatherConditionDto>
    {
        private readonly EuropeWeatherEntities _context;
        public WeatherConditionsToWeatherConditionDtoMapping(EuropeWeatherEntities context)
        {
            _context = context;
        }

        public WeatherConditionDto To(WeatherConditions source)
        {
            return new WeatherConditionDto
            {
                Id = source.WeatherConditionId,
                Name = source.Description,
                ExternalId = source.ExternalId,
            };
        }

        public WeatherConditions From(WeatherConditionDto destination)
        {
            var condition = destination.Id > 0 ? _context.WeatherConditions.Find(destination.Id) : _context.WeatherConditions.Create();
            condition.Description = destination.Name;
            condition.ExternalId = destination.ExternalId;

            return condition;
        }


        public WeatherConditions FromId(int id)
        {
            var condition = _context.WeatherConditions.Find(id);
            return condition;
        }

        public WeatherConditions FromExternalId(int externalId)
        {
            var condition = _context.WeatherConditions.FirstOrDefault(w => w.ExternalId == externalId);
            return condition;
        }
    }
}