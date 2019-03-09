using System;
using System.Threading.Tasks;
using MassTransit;
using Commonlib;
using System.Globalization;

namespace OrderService
{
    public class OrderReceivedConsumer : IConsumer<IOrderReceivedEvent>
    {
        public async Task Consume(ConsumeContext<IOrderReceivedEvent> context)
        {
            var orderCommand = context.Message;

            await Console.Out.WriteLineAsync($"OrderService:Order code: {orderCommand.CorrelationId} Order id: {orderCommand.OrderCode} is received.{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}");

            //do something..

            await context.Publish<IOrderProcessedEvent>(
                new { CorrelationId = context.Message.CorrelationId, OrderId = orderCommand.OrderId });
        }
    }
}