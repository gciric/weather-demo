//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EuropeWeather.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class WeatherDataConditions
    {
        public long WeatherDataConditionId { get; set; }
        public long WeatherDataId { get; set; }
        public int WeatherConditionId { get; set; }
        public string WeatherMain { get; set; }
        public string WeatherIcon { get; set; }
    
        public virtual WeatherConditions WeatherConditions { get; set; }
        public virtual WeatherData WeatherData { get; set; }
    }
}
