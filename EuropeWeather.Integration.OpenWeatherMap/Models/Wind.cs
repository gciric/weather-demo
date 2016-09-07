using Newtonsoft.Json;

namespace EuropeWeather.Integration.OpenWeatherMap.Models
{
    public class Wind
    {

        [JsonProperty("speed")]
        public decimal? Speed { get; set; }

        [JsonProperty("deg")]
        public decimal? Deg { get; set; }

        [JsonProperty("var_beg")]
        public int? VarBeg { get; set; }

        [JsonProperty("var_end")]
        public int? VarEnd { get; set; }
    }
}