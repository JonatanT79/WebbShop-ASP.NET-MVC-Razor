using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebbShop.Data;
using WebbShop.Models;
using WebbShop.Services;

namespace WebbShop.Controllers
{
    public class OrderController : Controller
    {
        OrderService _orderService = new OrderService();
        readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> OrderHistory()
        {
            OrderHistoryViewModel _orderHistoryVM = new OrderHistoryViewModel();

            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _orderHistoryVM.OrderList = await _orderService.GetAllOrdersByUserIDAsync(UserID);

            return View(_orderHistoryVM);
        }

        [HttpPost]
        public async Task<IActionResult> InsertConfirmedOrder(decimal TotalSum, string Shipment)
        {
            var Order = CreateConfirmedOrder(TotalSum);
            await _orderService.InsertOrderAsync(Order);

            var OrderID = Order.OrderID;
            var OrderItems = ProductsInConfirmedOrder();
            await _orderService.InsertOrderItemsAsync(OrderItems, OrderID);
            Response.Cookies.Delete("Cart");

            if (Shipment == "Send")
            {
                return RedirectToAction("ShipmentAddress", "Order");
            }

            return RedirectToAction("CompleteOrder", "Order");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSingleOrderHistory(Guid OrderID)
        {
            await _orderService.DeleteOrderAsync(OrderID);
            return RedirectToAction("OrderHistory", "Order");
        }

        [HttpGet]
        public IActionResult ShipmentAddress()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShipmentAddress(string Firstname, string Lastname, string Address, string City, string PostalCode)
        {
            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var GetUserAddress = _context.UserAddress.Where(e => e.UserID == UserID).SingleOrDefault();

            GetUserAddress.FirstName = Firstname;
            GetUserAddress.LastName = Lastname;
            GetUserAddress.Address = Address;
            GetUserAddress.City = City;
            GetUserAddress.PostalCode = PostalCode;
            GetUserAddress.UserID = UserID;

            _context.SaveChanges();

            return RedirectToAction("CompleteOrder", "Order");
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
