using System;
using System.Linq;
using EuropeWeather.BusinessLogic.Interfaces;
using EuropeWeather.DataAccess;
using EuropeWeather.Domain.Models;

namespace EuropeWeather.BusinessLogic.Mappings
{
    public class WeatherDataToWeatherDataDtoMapping : IMapping<WeatherData, WeatherDataDto>
    {
        private readonly EuropeWeatherEntities _context;
        public WeatherDataToWeatherDataDtoMapping(EuropeWeatherEntities context)
        {
            _context = context;
        }

        public WeatherData From(WeatherDataDto destination)
        {
            throw new NotImplementedException();
        }

        public WeatherDataDto To(WeatherData source)
        {
            var cityMapper = new CitiesToCityDtoMapping(_context);
            var conditionMapper = new WeatherDataConditionToWeatherDataConditionDtoMapping(_context);
            var city = cityMapper.To(source.Cities);
            var conditions = source.WeatherDataConditions.Select(w => conditionMapper.To(w)).ToList();
            return new WeatherDataDto
            {
                Id = source.WeatherDataId,
                City = city,
                Conditions = conditions,
                Temperature = source.Temperature,
                Pressure = source.Pressure,
                Humidity = source.Humidity,
                MinTemperature = source.MinTemperature,
                MaxTemperature = source.MaxTemperature,
                Visibility = source.Visibility,
                WindSpeed = source.WindSpeed,
                WindDirection = source.WindDirection,
                Clouds = source.Clouds,
                Rain = source.Rain,
                Snow = source.Snow,
                Sunrise = source.Sunrise,
                Sunset = source.Sunset,
                TimeOfCalculation = source.TimeOfCalculation,
                Created = source.Created
            };
        }
    }
}
