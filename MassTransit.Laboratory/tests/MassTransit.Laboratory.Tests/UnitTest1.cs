using MassTransit.Laboratory.Api.Consumers;
using MassTransit.Laboratory.Api.Models;
using NSubstitute;

namespace MassTransit.Laboratory.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var messages = new List<WeatherForecast>
            {
                new WeatherForecast { Text = "message" },
                new WeatherForecast { Text = "message1" },
            };

            var batchMessages = new List<ConsumeContext<WeatherForecast>>();
            foreach (var message in messages)
            {
                var messageContext = Substitute.For<ConsumeContext<WeatherForecast>>();
                messageContext.Message.Returns(message);
                batchMessages.Add(messageContext);
            }

            var batch = Substitute.For<Batch<WeatherForecast>>();
            batch.Length.Returns(batchMessages.Count);
            batch.GetEnumerator().Returns(batchMessages.GetEnumerator());

            var consumeContext = Substitute.For<ConsumeContext<Batch<WeatherForecast>>>();
            consumeContext.Message.Returns(batch);

            var consumer = new WeatherForecastConsumer();

            // Act
            await consumer.Consume(consumeContext);

            // Assert
            Assert.True(messages.Any());
        }
    }
}