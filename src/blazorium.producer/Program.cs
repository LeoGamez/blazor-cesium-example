// See https://aka.ms/new-console-template for more information
using blazorium.producer.Services;
using blazorium.Shared.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory()
{
    Uri = new Uri("amqp://guest:guest@rabbitmq:5672")
};

const string queue = "weather-queue";
var weatherPoints = new List<WeatherPoint>()
{
    new WeatherPoint()
    {
        Id=1,
        Name="Austin",
        Longitude=-97.7431,
        Latitude=30.2672,
        Country="US"
    },
    new WeatherPoint()
    {
        Id=2,
        Name="Seattle",
        Longitude=-122.3321,
        Latitude=47.6,
        Country="US"
    },
    new WeatherPoint()
    {
        Id=3,
        Name="Los Angeles",
        Longitude=-118.2437,
        Latitude=34.0522,
        Country="US"
    }
    ,
    new WeatherPoint()
    {
        Id=4,
        Name="NY City",
        Longitude=-74.006,
        Latitude=40.7128,
        Country="US"
    }
};
var enable = true;

while (enable)
{
    using (var connection = factory.CreateConnection())
    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare(
            queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var weatherService = new WeatherService();

        weatherPoints = await weatherService.GetWeatherPointsAsync(weatherPoints);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(weatherPoints));
        channel.BasicPublish(
            exchange: "",
            routingKey: queue,
            basicProperties: null,
            body: body
        );

        await Task.Delay(3000);

    }
}
