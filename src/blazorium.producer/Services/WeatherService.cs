using blazorium.Shared.Model;
using Flurl.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazorium.producer.Services
{
    public class WeatherService
    {
        private readonly Random random;

        public WeatherService()
        {
            this.random = new Random();
        }

        public async Task<List<WeatherPoint>> GetWeatherPointsAsync(List<WeatherPoint> weatherPoints)
        {
            foreach(WeatherPoint weatherPoint in weatherPoints)
            {
                var url = $"https://openweathermap.org/data/2.5/onecall?lat={weatherPoint.Latitude}&lon={weatherPoint.Longitude}&units=metric&appid=439d4b804bc8187953eb36d2a8c26a02";
                var result = await url.GetStringAsync();
                var rand = (double)((double)random.Next(0, 100) / (double)100);
                var weatherJson = JObject.Parse(result);
                weatherPoint.TemperatureC = Math.Round(((double)weatherJson["current"]["temp"]) - 273.15 + rand, 2);
            }        

            return weatherPoints;
        }

    }
}
