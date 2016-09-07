using System;
using System.Collections.Generic;
using EuropeWeather.Integration.OpenWeatherMap.Interfaces;
using EuropeWeather.Integration.OpenWeatherMap.Models;
using Newtonsoft.Json;
using RestSharp;

namespace EuropeWeather.Integration.OpenWeatherMap
{
    public class CurrentWeather : ICurrentWeather
    {

        public ApiResult GetCurrentWeather(string url, IEnumerable<int> ids, string token)
        {
            var client = new RestClient($"{url}group");
            var request = new RestRequest(Method.GET);
            request.AddQueryParameter("id", string.Join(",", ids));
            request.AddQueryParameter("APPID", token);
            request.AddHeader("cache-control", "no-cache");
            var response = client.Execute(request);

            if(response.ErrorException == null)
            {
                return JsonConvert.DeserializeObject<ApiResult>(response.Content);
            }

            const string message = "Error retrieving response.  Check inner details for more info.";
            var exception = new ApplicationException(message, response.ErrorException);
            throw exception;
        }

        public ApiData GetCurrentWeather(string url, int cityId, string token)
        {
            var client = new RestClient($"{url}weather");

            var request = new RestRequest(Method.GET);
            request.AddQueryParameter("id", cityId.ToString());
            request.AddQueryParameter("APPID", token);
            request.AddHeader("cache-control", "no-cache");
            var response = client.Execute(request);

            if (response.ErrorException == null)
            {
                return JsonConvert.DeserializeObject<ApiData>(response.Content);
            }

            const string message = "Error retrieving response.  Check inner details for more info.";
            var exception = new ApplicationException(message, response.ErrorException);
            throw exception;
        }

    }
}
