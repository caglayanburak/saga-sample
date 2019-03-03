using System;
using MassTransit;

namespace BillingService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "BillingService";

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
  var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
  {
      h.Username("guest");
      h.Password("guest");
  });

  cfg.ReceiveEndpoint(host, "trendyol_saga", e =>
  {
      e.Consumer<OrderProcessedConsumer>();
  });
});
            busControl.Start();
            Console.ReadLine();
        }
    }
}
