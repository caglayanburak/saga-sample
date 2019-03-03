using System;
using System.Threading.Tasks;
using MassTransit;
using Commonlib;

namespace OrderService
{
    public class OrderReceivedConsumer : IConsumer<OrderModel>
    {
        public async Task Consume(ConsumeContext<OrderModel> context)
        {
            var orderCommand = context.Message;

            await Console.Out.WriteLineAsync($"OrderService: {DateTime.Now}Order code: {orderCommand.CorrelationId} Order id: {orderCommand.OrderId} is received.");

            //do something..

            await context.Publish<IOrderProcessedEvent>(
                new { CorrelationId = context.Message.CorrelationId, OrderId = orderCommand.OrderId });
        }
    }
}