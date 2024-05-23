using MassTransit.Laboratory.Api.Models;

namespace MassTransit.Laboratory.Api.Consumers
{
    public class WeatherForecastConsumer : IConsumer<Batch<WeatherForecast>>
    {
        public Task Consume(ConsumeContext<Batch<WeatherForecast>> context)
        {
            //Console.WriteLine($"Recebendo mensagem: {context.Message.Text}");
            throw new NotImplementedException();
        }
    }
}
