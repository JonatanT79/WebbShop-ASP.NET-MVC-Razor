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
        ProductService _productService = new ProductService();

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
            var Order = CreateConfirmedOrder(TotalSum);
            await _orderService.InsertOrder(Order);

            var OrderID = Order.OrderID;
            var OrderItems = ProductsInConfirmedOrder();
            await _orderService.InsertOrderItems(OrderItems, OrderID);

            Request.Cookies.SingleOrDefault(s => s.Key == "Cart");
            Response.Cookies.Delete("Cart");

            return RedirectToAction("CompleteOrder", "Order");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSingleOrderHistory(Guid OrderID)
        {
            await _orderService.DeleteOrder(OrderID);
            return RedirectToAction("OrderHistory", "Order");
        }

        [HttpGet]
        public IActionResult CompleteOrder()
        {
            return View();
        }

        public Order CreateConfirmedOrder(decimal TotalSum)
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Order _order = new Order() { OrderID = Guid.NewGuid(), OrderMadeAt = DateTime.Now, TotalSum = (float)TotalSum, UserID = UserID };
            return _order;
        }

        public List<int> ProductsInConfirmedOrder()
        {
            List<Products> ListOfProductIDs = new List<Products>();
            var Cart = Request.Cookies.SingleOrDefault(c => c.Key == "Cart");
            string Cookiestring = Cart.Value;
            var ProductIDs = Cookiestring.Split(",").Select(s => int.Parse(s)).ToList();

            return ProductIDs;
        }
    }
}
