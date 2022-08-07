using blazorium.Shared.Model;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.Text;

namespace blazorium.Server.Services
{
    public class WeatherService : BackgroundService
    {
        const string queue = "weather-queue";

        private IConnection connection;
        private IModel channel;
        public Func<List<WeatherPoint>, Task>? WeatherPointsCallBack { get; private set; }

        public WeatherService()
        {
            var connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@rabbitmq:5672")

            };

            this.connection = connectionFactory.CreateConnection();
            this.channel = connection.CreateModel();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);
           
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var weatherPoints = JsonConvert.DeserializeObject<List<WeatherPoint>>(message);

                if(WeatherPointsCallBack != null)
                {
                    WeatherPointsCallBack(weatherPoints);
                }
            };      

            channel.BasicConsume(
             queue: queue,
             autoAck: true,
             consumer: consumer);
            return Task.CompletedTask;
        }

        public void SetCallBack(Func<List<WeatherPoint>,Task> callback)
        {
            this.WeatherPointsCallBack = callback;
        }
    }

}
