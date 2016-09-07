using System;
using EuropeWeather.BusinessLogic.Interfaces;
using EuropeWeather.DataAccess;
using EuropeWeather.Domain.Models;

namespace EuropeWeather.BusinessLogic.Mappings
{
    public class WeatherDataConditionToWeatherDataConditionDtoMapping :
        IMapping<WeatherDataConditions, WeatherDataConditionDto>
    {
        private readonly EuropeWeatherEntities _context;
        public WeatherDataConditionToWeatherDataConditionDtoMapping(EuropeWeatherEntities context)
        {
            _context = context;
        }

        public WeatherDataConditions From(WeatherDataConditionDto destination)
        {
            throw new NotImplementedException();
        }

        public WeatherDataConditionDto To(WeatherDataConditions source)
        {
            var mapper = new WeatherConditionsToWeatherConditionDtoMapping(_context);
            return new WeatherDataConditionDto
            {
                WeatherCondition = mapper.To(source.WeatherConditions),
                Id = source.WeatherDataConditionId,
                Icon = source.WeatherIcon,
                Main = source.WeatherMain
            };
        }
    }
}