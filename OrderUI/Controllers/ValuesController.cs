using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Commonlib;

namespace OrderUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private readonly IBusControl _bus;

        public OrderController(IBusControl bus)
        {
            _bus = bus;
            // var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            // {
            //     var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
            //       {
            //           h.Username("guest");
            //           h.Password("guest");
            //       });

            // });
            // var sendToUri = new Uri($"rabbitmq://localhost/sagasample");
            // busControl.Start();
            // _bus = busControl.GetSendEndpoint(sendToUri).Result;

        }

        // GET: Order
        [HttpGet]
        public IActionResult Index(string code)
        {
            OrderModel orderModel = new OrderModel();
            orderModel.OrderCode = code;
            orderModel.OrderId = 1;
            orderModel.CorrelationId = Guid.NewGuid();
            // if (orderModel.OrderId > 0)
            CreateOrder(orderModel);

            return Ok(orderModel);
        }

        private void CreateOrder(OrderModel orderModel)
        {
            _bus.Send(orderModel).Wait();
        }
    }
}
