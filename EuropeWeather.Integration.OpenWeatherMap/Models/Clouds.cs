using Newtonsoft.Json;

namespace EuropeWeather.Integration.OpenWeatherMap.Models
{
    public class Clouds
    {

        [JsonProperty("all")]
        public int? All { get; set; }
    }
}