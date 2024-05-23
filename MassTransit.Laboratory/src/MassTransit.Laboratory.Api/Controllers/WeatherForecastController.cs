using MassTransit.Internals.Caching;
using MassTransit.Laboratory.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Laboratory.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IBusControl _bus;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBusControl bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost]
        public async  Task<IActionResult> Post([FromBody] IEnumerable<WeatherForecast> messages)
        {
            foreach (var item in messages)
            {
                await _bus.Publish(item);
            }

            return Ok("Message sent");
        }
    }
}
