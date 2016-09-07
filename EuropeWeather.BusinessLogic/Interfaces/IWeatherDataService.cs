using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EuropeWeather.Domain.Interfaces;

namespace EuropeWeather.BusinessLogic.Interfaces
{
    public interface IWeatherDataService
    {
        Task<bool> ImportWeatherData(string url, string apiKe);
        Task<bool> ImportWeatherData(string url, string apiKe, int cityId);
        Task<IEnumerable<ICity>> GetCities();
        Task<ICity> GetCity(int cityId);
        Task<IEnumerable<IWeatherData>> GetWeatherDataForDate(DateTime date);
        Task<IEnumerable<IWeatherData>> SearchWeatherData(IEnumerable<int> cities = null, TimeSpan? sunsetFrom = null,
            TimeSpan? sunsetTo = null, TimeSpan? sunriseFrom = null, TimeSpan? sunriseTo = null, DateTime? from = null,
            DateTime? to = null, int? minTemp = null, int? maxTemp = null);
        Task<IEnumerable<IWeatherData>> GetLatestWeatherData(IEnumerable<int> cities = null, TimeSpan? sunsetFrom = null,
            TimeSpan? sunsetTo = null, TimeSpan? sunriseFrom = null, TimeSpan? sunriseTo = null, DateTime? from = null,
            DateTime? to = null, int? minTemp = null, int? maxTemp = null);
    }
}
