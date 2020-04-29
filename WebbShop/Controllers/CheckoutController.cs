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
           // klistra in denna nedan senare , new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(60.0) }
            Response.Cookies.Append("Cart", cookiestring);
            _productdetails.Productlist = Data.GetList();

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
            ProductDetails _prodcuctDetails = new ProductDetails();
            var Cart = Request.Cookies.SingleOrDefault(c => c.Key == "Cart");
            string CookieValue = Cart.Value;
            var ProductIDs = CookieValue.Split(",").Select(s => int.Parse(s));

            // Get the list of values
           _prodcuctDetails.Productlist = Data.GetList();
            foreach (var item in ProductIDs)
            {
                _prodcuctDetails.CartList.Add(_prodcuctDetails.Productlist[item - 1]);
            }
            return View(_prodcuctDetails);
        }
        public IActionResult CompleteOrder()
        {
            return View();
        }
    }
}