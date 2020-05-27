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
    }
}