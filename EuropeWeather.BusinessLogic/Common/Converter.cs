using System;

namespace EuropeWeather.BusinessLogic.Common
{
    public static class Converter
    {
        private const decimal ABSOLUTE_ZERO = (decimal)273.15;

        private const decimal FAHRENHEIT_CONST = (decimal)459.67;

        private const decimal MPS_TO_MPH_BASE = (decimal)2.236936;

        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static decimal FromKelvinToCelsius(decimal kelvin)
        {
            return kelvin - ABSOLUTE_ZERO;
        }

        public static decimal FromCelsiusToKelvin(decimal celsius)
        {
            return celsius + ABSOLUTE_ZERO;
        }

        public static decimal FromKelvinToFahrenheit(decimal kelvin)
        {
            return kelvin * (decimal)1.8 - FAHRENHEIT_CONST;
        }

        public static decimal FromFahrenheitToKelvin(decimal fahrenheit)
        {
            return (fahrenheit + FAHRENHEIT_CONST) / (decimal)1.8;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            //var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            //return dtDateTime.AddSeconds(unixTimeStamp);
            
            var timeSpan = TimeSpan.FromSeconds(unixTimeStamp);
            return Epoch.Add(timeSpan);
        }

        public static decimal MetersPerSecondToMilesPerHour(decimal mps)
        {
            return mps * MPS_TO_MPH_BASE;
        }
    }
}
