using Confluent.Kafka;
using KafKa.Net;
using KafKa.Net.Abstractions;
using KafKa.Net.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APIGATEWAY.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IKafKaConnection _kafKaConnection;
        private readonly IEventBus _eventBus;

        public WeatherForecastController(IEventBus eventBus,IKafKaConnection kafKaConnection, ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _kafKaConnection = kafKaConnection;
            _eventBus = eventBus;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
       
        [HttpGet("test")]
        public IActionResult Test(string name)
        {
            //var model = new ttt
            //{
            //    name = name,
            //    age = DateTime.Now.ToString()
            //};
            //if (!_kafKaConnection.IsConnected)
            //    _kafKaConnection.TryConnect();
            //IProducer<string, byte[]>? producer = null;
            //producer = _kafKaConnection.ProducerConfig;

            //var body = JsonSerializer.SerializeToUtf8Bytes(model, model.GetType(), new JsonSerializerOptions
            //{
            //    WriteIndented = true
            //});
            //producer.Produce("WareHouse-KafKa", new Message<string, byte[]> { Key = model.GetType().ToString(), Value = body });

            //Console.WriteLine($"Wrote to offset: ");
            //producer.Flush(timeout: TimeSpan.FromSeconds(1));

            //  return Ok(model);

            var mode = new TestIntegrationEvent()
            {
                Username = name
            };
            _eventBus.Publish(mode);
            return Ok(mode);
        }
    }
}