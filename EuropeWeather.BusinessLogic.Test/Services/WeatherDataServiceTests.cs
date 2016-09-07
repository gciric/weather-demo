using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace EuropeWeather.BusinessLogic.Services.Tests
{
    [TestClass()]
    public class WeatherDataServiceTests
    {
        private const string Url = "http://api.openweathermap.org/data/2.5/";
        private const string ApiKey = "01f080df80b285cbcbcbb56630df970f";

        [TestMethod()]
        public void ImportWeatherDataTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public async void ImportWeatherDataForOneCityTest()
        {
            const int cityId = 18;
            var service = new WeatherDataService();
            var result = await service.ImportWeatherData(Url, ApiKey, cityId);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public async void GetCitiesTest()
        {
            var service = new WeatherDataService();
            var list = await service.GetCities();
            Assert.AreEqual(list.Count(), 51);
        }

        [TestMethod()]
        public void GetCityTest()
        {
            var service = new WeatherDataService();
            var city = service.GetCity(1);
            Assert.IsNotNull(city);
        }

        [TestMethod()]
        public void GetWeatherDataForDateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetLatestWeatherDataTest()
        {
            Assert.Fail();
        }
    }
}