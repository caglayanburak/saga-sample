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
            _bus.Start();
        }

        // GET: Order
        [HttpGet]
        public IActionResult Index(string code,int id)
        {
            OrderModel orderModel = new OrderModel();
            orderModel.OrderCode = code;
            orderModel.OrderId = id;
            orderModel.CorrelationId = Guid.NewGuid();
            CreateOrder(orderModel);

            return Ok(orderModel);
        }

        private void CreateOrder(OrderModel orderModel)
        {
            _bus.Send(orderModel).Wait();
        }
    }
}
