using Microsoft.AspNetCore.Mvc;
using Order.API.Data;
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
        readonly OrderContext _context = new OrderContext();

        [HttpGet]
        public IActionResult GetOrders()
        {
            var OrdersList = _orderRepository.GetAllOrders();
            return Ok(OrdersList);
        }

        [HttpGet("Items/{OrderID}")]
        public IActionResult GetAllOrderItems(Guid OrderID)
        {
            var OrderItemsList = _orderRepository.GetAllOrderItemsByOrderID(OrderID);
            return Ok(OrderItemsList);
        }

        [HttpGet("{UserID}")]
        public IActionResult GetAllOrdersByUserID(string UserID)
        {
            var UserOrders = _orderRepository.GetAllOrdersByUserID(UserID);
            return Ok(UserOrders);
        }

        [HttpGet("Single/{OrderID}")]
        public IActionResult GetOrderByOrderID(Guid OrderID)
        {
            if (_context.Orders.Any(a => a.OrderID == OrderID))
            {
                var Order = _orderRepository.GetOrderByOrderID(OrderID);
                return Ok(Order);
            }
            else
            {
                return NotFound("No order have that ID");
            }
        }

        [HttpPost("InsertOrder")]
        public IActionResult CreateOrder([FromBody] Orders order)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _orderRepository.CreateOrder(order);
                scope.Complete();
                return CreatedAtAction(nameof(CreateOrder), new { OrderID = order.OrderID }, order);
            }
        }

        [HttpPost("InsertItems/{OrderID}")]
        public IActionResult AddOrderItems([FromBody] List<int> Items, Guid OrderID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _orderRepository.InsertOrderItems(Items, OrderID);
                scope.Complete();
                return CreatedAtAction(nameof(AddOrderItems), new { OrderID = OrderID }, Items);
            }
        }

        [HttpDelete("Delete/{OrderID}")]
        public IActionResult DeleteOrder(Guid OrderID)
        {
            if (_context.Orders.Any(a => a.OrderID == OrderID))
            {
                _orderRepository.DeleteSingleOrderFromHistory(OrderID);
                return Ok(_orderRepository.GetAllOrders());
            }
            else
            {
                return NotFound("No order have that ID");
            }
        }

        [HttpDelete("Delete/ByUserID/{UserID}")]
        public IActionResult DeleteAllUserOrders(string UserID)
        {
            _orderRepository.DeleteAllUserOrders(UserID);
            return Ok(_orderRepository.GetAllOrders());
        }

        [HttpPut("Update")]
        public IActionResult UpdateOrder([FromBody] Orders order)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _orderRepository.UpdateOrder(order);
                scope.Complete();
                return Ok();
            }
        }
    }
}
