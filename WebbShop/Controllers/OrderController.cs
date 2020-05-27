using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebbShop.Models;
using WebbShop.Services;

namespace WebbShop.Controllers
{
    public class OrderController : Controller
    {
        OrderService _orderService = new OrderService();
        public async Task<IActionResult> OrderHistory()
        {
            var OrdersList = await _orderService.GetAllOrders();
            return View(OrdersList);
        }
    }
}