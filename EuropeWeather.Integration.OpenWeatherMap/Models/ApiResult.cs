using System.Collections.Generic;
using Newtonsoft.Json;

namespace EuropeWeather.Integration.OpenWeatherMap.Models
{
    public class ApiResult
    {

        [JsonProperty("cnt")]
        public int Count { get; set; }

        [JsonProperty("list")]
        public IList<ApiData> List { get; set; }
    }



}
