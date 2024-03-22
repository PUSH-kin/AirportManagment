using AirportService.Domain.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AirportService.Domain.Consumers
{
    public class SimpleConsumer : IConsumer<Message>
    {
        readonly ILogger<SimpleConsumer> _logger;

        public SimpleConsumer(ILogger<SimpleConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<Message> context)
        {
            _logger.LogInformation("Received Text: {Text}", JsonConvert.SerializeObject(context.Message, Formatting.Indented));
            return Task.CompletedTask;
        }
    }
}
