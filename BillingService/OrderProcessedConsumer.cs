using System;
using System.Threading.Tasks;
using MassTransit;
using Commonlib;

namespace BillingService
{
    public class OrderProcessedConsumer : IConsumer<IOrderProcessedEvent>
    {
        public async Task Consume(ConsumeContext<IOrderProcessedEvent> context)
        {
            var orderCommand = context.Message;

            await Console.Out.WriteLineAsync($"BillingService:{DateTime.Now} Order code: {orderCommand.CorrelationId} Order id: {orderCommand.OrderId} is received.");

            //do something..

            // await context.Publish(
            //     new { CorrelationId = context.Message.CorrelationId, OrderId = orderCommand.OrderId });
        }
    }
}