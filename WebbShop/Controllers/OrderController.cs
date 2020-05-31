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
            OrderHistoryViewModel _orderHistoryVM = new OrderHistoryViewModel();

            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _orderHistoryVM.OrderList = await _orderService.GetAllOrdersByUserIDAsync(UserID);

            return View(_orderHistoryVM);
        }

        [HttpGet]
        public async Task<IActionResult> InsertConfirmedOrder(decimal TotalSum)
        {
            var Order = CreateConfirmedOrder(TotalSum);
            await _orderService.InsertOrderAsync(Order);

            var OrderID = Order.OrderID;
            var OrderItems = ProductsInConfirmedOrder();
            await _orderService.InsertOrderItemsAsync(OrderItems, OrderID);

            Request.Cookies.SingleOrDefault(s => s.Key == "Cart");
            Response.Cookies.Delete("Cart");

            return RedirectToAction("CompleteOrder", "Order");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSingleOrderHistory(Guid OrderID)
        {
            await _orderService.DeleteOrderAsync(OrderID);
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
