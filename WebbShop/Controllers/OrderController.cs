using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebbShop.Models;
using WebbShop.Services;

namespace WebbShop.Controllers
{
    public class OrderController : Controller
    {
        OrderService _orderService = new OrderService();

        [HttpGet]
        public async Task<IActionResult> OrderHistory()
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var OrdersList = await _orderService.GetAllOrdersByUserID(UserID);

            return View(OrdersList);
        }

        [HttpGet]
        public async Task<IActionResult> InsertConfirmedOrder(decimal TotalSum)
        {
            var Order = CreateTheOrderFromCart(TotalSum);
            await _orderService.InsertOrder(Order);

            return RedirectToAction("CompleteOrder", "Order");
        }

        [HttpGet]
        public IActionResult CompleteOrder()
        {
            return View();
        }
        public Order CreateTheOrderFromCart(decimal TotalSum)
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Order _order = new Order() { OrderID = Guid.NewGuid(), OrderMadeAt = DateTime.Now, TotalSum = (float)TotalSum, UserID = UserID };
            return _order;
        }
    }
}