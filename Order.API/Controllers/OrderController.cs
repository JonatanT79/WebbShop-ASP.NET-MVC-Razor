using Microsoft.AspNetCore.Mvc;
using Order.API.Models;
using Order.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        OrderRepository _orderRepository = new OrderRepository();

        [HttpGet]
        public IActionResult GetOrders()
        {
            var OrdersList = _orderRepository.GetAllOrders();
            return Ok(OrdersList);
        }

        [HttpGet("{ID}")]
        public IActionResult GetOrderByID(Guid ID)
        {
            var Order = _orderRepository.GetOrderByID(ID);
            return Ok(Order);
        }
    }
}
// skapa ett orderitem klass och en foreign i 'orderitem' klassen
