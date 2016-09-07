using Newtonsoft.Json;

namespace EuropeWeather.Integration.OpenWeatherMap.Models
{
    public class Main
    {

        [JsonProperty("temp")]
        public decimal? Temp { get; set; }

        [JsonProperty("humidity")]
        public int? Humidity { get; set; }

        [JsonProperty("pressure")]
        public int? Pressure { get; set; }

        [JsonProperty("temp_min")]
        public decimal? TempMin { get; set; }

        [JsonProperty("temp_max")]
        public decimal? TempMax { get; set; }
    }
}