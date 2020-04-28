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
            //CartSign = true if user pressed the cartsign
            var Cart = Request.Cookies.SingleOrDefault(c => c.Key == "Cart");
            string cookiestring = Cart.Value + "";

            if (CartSign == false && Cart.Value != "" && Cart.Value != null)
            {
                cookiestring = Cart.Value + "," + ID;
            }
            else if(CartSign == false)
            {
                cookiestring = ID.ToString();
            }

            Response.Cookies.Append("Cart", cookiestring);
            ProductDetails _productdetails = new ProductDetails();
            //Test data, OBS, ifsatsen ska bytas ut mot LINQ senare när databasen finns
            if (ID == 1)
            {
                _productdetails.Name = "Dator";
                _productdetails.Description = "Stationär";
                _productdetails.Price = 14999M;
                var DateAndTime = DateTime.Now;
                _productdetails.Release = DateAndTime.Date;
                _productdetails.Maker = "MSI";
            }
            else if (ID == 2)
            {
                _productdetails.Name = "TV";
                _productdetails.Description = "55 Tum";
                _productdetails.Price = 8999M;
                var DateAndTime = DateTime.Now;
                _productdetails.Release = DateAndTime.Date;
                _productdetails.Maker = "Philips";
            }
            else if (ID == 3)
            {
                _productdetails.Name = "Hörlurar";
                _productdetails.Description = "Iphone";
                _productdetails.Price = 799M;
                var DateAndTime = DateTime.Now;
                _productdetails.Release = DateAndTime.Date;
                _productdetails.Maker = "Apple";
            }
            return View();
        }
        [HttpPost]
        public IActionResult ShoppingCart()
        {
            Response.Cookies.Delete("Cart");
            return View();
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