using System;
using MassTransit;
using MassTransit.Saga;

namespace LCSaga
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Saga";
            var orderSaga = new OrderSaga();
            var repo = new InMemorySagaRepository<OrderSagaState>();

 var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
  var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
  {
      h.Username("guest");
      h.Password("guest");
  });

  cfg.ReceiveEndpoint(host, "trendyol_saga", e =>
  {
      e.StateMachineSaga(orderSaga, repo);
  });
  cfg.UseInMemoryOutbox();
});
            busControl.Start();

            Console.WriteLine("Order saga started..");
            Console.ReadLine();
        }
    }
}
