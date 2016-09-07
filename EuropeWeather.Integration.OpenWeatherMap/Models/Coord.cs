using Newtonsoft.Json;

namespace EuropeWeather.Integration.OpenWeatherMap.Models
{
    public class Coord
    {

        [JsonProperty("lon")]
        public decimal? Lon { get; set; }

        [JsonProperty("lat")]
        public decimal? Lat { get; set; }
    }
}