using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebbShop.Models;

namespace WebbShop.Controllers
{
    public class CheckoutController : Controller
    {
        [HttpGet]
        public IActionResult ShoppingCart(int? ID, bool CartSign)
        {
            //Test data, OBS!, Koden ska skrivas om senare när databasen finns ********************************************
            //CartSign = true if user pressed the cartsign
            ProductDetails _productdetails = new ProductDetails();

            var Cart = Request.Cookies.SingleOrDefault(c => c.Key == "Cart");
            string cookiestring = Cart.Value + "";

            if (CartSign == false && Cart.Value != "" && Cart.Value != null)
            {
                cookiestring = Cart.Value + "," + ID;
            }
            else if (CartSign == false)
            {
                cookiestring = ID.ToString();
            }

            Response.Cookies.Append("Cart", cookiestring);
            _productdetails.Productlist = new List<Products>()
            {
                new Products() { ID = 1, Name = "Dator", Description = "Stationär", Price = 14999M },
                new Products() { ID = 2, Name = "TV", Description = "55 Tum", Price = 8999M },
                new Products() { ID = 3, Name = "Hörlurar", Description = "Iphone", Price = 799M },
            };

            if (cookiestring != "")
            {
                var productIds = cookiestring.Split(",").Select(c => int.Parse(c));

                foreach (var item in productIds)
                {
                    _productdetails.CartList.Add(_productdetails.Productlist[item - 1]);
                    _productdetails.Totalsum += _productdetails.Productlist[item - 1].Price;
                }
            }

            return View(_productdetails);
        }
        [HttpPost]
        public IActionResult ShoppingCart()
        {
            ProductDetails _productDetails = new ProductDetails();
            Response.Cookies.Delete("Cart");

            return View(_productDetails);
        }
        public IActionResult ConfirmOrder()
        {
            return View();
        }
        public IActionResult CompleteOrder()
        {
            return View();
        }
    }
}