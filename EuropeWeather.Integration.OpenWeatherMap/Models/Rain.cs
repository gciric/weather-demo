using Newtonsoft.Json;

namespace EuropeWeather.Integration.OpenWeatherMap.Models
{
    public class Rain
    {
        [JsonProperty("3h")]
        public decimal? _3H { get; set; }
    }
}