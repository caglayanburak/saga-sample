using System;
using MassTransit;
using System.Threading.Tasks;

namespace OrderService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Order Service Start");
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
    {
        h.Username("guest");
        h.Password("guest");
    });

    cfg.ReceiveEndpoint(host, "trendyol_saga", e =>
    {
        e.Consumer<OrderReceivedConsumer>();
    });
});
            busControl.Start();
            Console.ReadLine();
        }
    }


}
