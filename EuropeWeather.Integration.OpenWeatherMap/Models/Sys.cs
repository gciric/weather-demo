using Newtonsoft.Json;

namespace EuropeWeather.Integration.OpenWeatherMap.Models
{
    public class Sys
    {

        [JsonProperty("type")]
        public int? Type { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("message")]
        public decimal? Message { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("sunrise")]
        public int? Sunrise { get; set; }

        [JsonProperty("sunset")]
        public int? Sunset { get; set; }
    }
}