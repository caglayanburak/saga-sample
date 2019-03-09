using System;
using MassTransit;

namespace BillingService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Order Billing Start");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "trendyol_saga_order", e =>
                    {
                        e.Consumer<OrderProcessedConsumer>();
                    });
            });
            busControl.Start();
            Console.ReadLine();
        }
    }
}
