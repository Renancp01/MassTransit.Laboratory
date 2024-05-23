using MassTransit.Laboratory.Api.Consumers;

namespace MassTransit.Laboratory.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(m =>
            {
                m.AddConsumer<WeatherForecastConsumer>(c =>
                {
                    c.Options<BatchOptions>(options =>
                    {
                        options.MessageLimit = 100; // Número máximo de mensagens por batch
                        options.TimeLimit = TimeSpan.FromSeconds(5); // Tempo máximo para esperar por mensagens no batch
                        options.ConcurrencyLimit = 10; // Número de batches concorrentes
                    });
                });

                m.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("user");
                        h.Password("password");
                    });

                    cfg.ReceiveEndpoint("WeatherForecast", e =>
                    {
                        e.ConfigureConsumer<WeatherForecastConsumer>(context);
                    });
                });
            });
        }
    }
}
