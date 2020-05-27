using Microsoft.AspNetCore.Mvc;
using Order.API.Models;
using Order.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

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

        [HttpGet("{UserID}")]
        public IActionResult GetAllOrdersByUserID(string UserID)
        {
            var UserOrders = _orderRepository.GetAllOrdersByUserID(UserID);
            return Ok(UserOrders);
        }

        [HttpGet("Single/{ID}")]
        public IActionResult GetOrderByID(Guid ID)
        {
            var Order = _orderRepository.GetOrderByID(ID);
            return Ok(Order);
        }

        [HttpPost("Insert")]
        public IActionResult CreateOrder([FromBody] Orders order)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _orderRepository.CreateOrder(order);
                scope.Complete();
                return CreatedAtAction(nameof(CreateOrder), new { OrderID = order.OrderID }, order);
            }
        }
    }
}
