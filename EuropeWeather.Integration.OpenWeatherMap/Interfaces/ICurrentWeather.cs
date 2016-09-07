using System.Collections.Generic;
using System.Threading.Tasks;
using EuropeWeather.Integration.OpenWeatherMap.Models;

namespace EuropeWeather.Integration.OpenWeatherMap.Interfaces
{
    public interface ICurrentWeather
    {
        ApiResult GetCurrentWeather(string url, IEnumerable<int> ids, string token);
        ApiData GetCurrentWeather(string url, int cityId, string token);
    }
}
