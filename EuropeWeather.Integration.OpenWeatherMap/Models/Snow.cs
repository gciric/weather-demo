using Newtonsoft.Json;

namespace EuropeWeather.Integration.OpenWeatherMap.Models
{
    public class Snow
    {
        [JsonProperty("3h")]
        public decimal? _3H { get; set; }
    }
}