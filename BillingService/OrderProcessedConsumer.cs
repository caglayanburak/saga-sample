using System;
using System.Threading.Tasks;
using MassTransit;
using Commonlib;
using System.Globalization;

namespace BillingService
{
    public class OrderProcessedConsumer : IConsumer<IOrderProcessedEvent>
    {
        public async Task Consume(ConsumeContext<IOrderProcessedEvent> context)
        {
            var orderCommand = context.Message;

            await Console.Out.WriteLineAsync($"BillingService: Order code: {orderCommand.CorrelationId} Order id: {orderCommand.OrderCode} is received.{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}");

            //do something..

            // await context.Publish(
            //     new { CorrelationId = context.Message.CorrelationId, OrderId = orderCommand.OrderId });
        }
    }
}