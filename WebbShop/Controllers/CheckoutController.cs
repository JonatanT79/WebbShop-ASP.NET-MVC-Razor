using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebbShop.Models;

namespace WebbShop.Controllers
{
    public class CheckoutController : Controller
    {
        [HttpGet]
        public IActionResult ShoppingCart(int? ID, bool CartSign = true)
        {
            ProductDetails _productdetails = new ProductDetails();

            var Cart = Request.Cookies.SingleOrDefault(c => c.Key == "Cart");
            string cookiestring = Cart.Value + "";

            if (CartSign == false && (!string.IsNullOrEmpty(Cart.Value)))
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

            if (!string.IsNullOrEmpty(cookiestring))
            {
                var productIds = cookiestring.Split(",").Select(c => int.Parse(c));

                foreach (var item in productIds)
                {
                    var ListElement = _productdetails.Productlist[item - 1];
                    var ListElementPrice = _productdetails.Productlist[item - 1].Price;

                    _productdetails.CartList.Add(ListElement);
                    _productdetails.Totalsum += ListElementPrice;
                }
            }

            return View(_productdetails);
        }
        [HttpPost]
        public IActionResult ShoppingCart(bool Isempty)
        {
            ProductDetails _productDetails = new ProductDetails();

            if (Isempty == true)
            {
                _productDetails.StatusMessage = "Cannot continue, your cart is empty!";
            }
            else
            {
                Response.Cookies.Delete("Cart");
            }
            return View(_productDetails);
        }
        public IActionResult ConfirmOrder()
        {
            ProductDetails _prodcuctDetails = new ProductDetails();
            var Cart = Request.Cookies.SingleOrDefault(c => c.Key == "Cart");
            string CookieValue = Cart.Value;

            var ProductIDs = CookieValue.Split(",").Select(s => int.Parse(s));
            _prodcuctDetails.Productlist = Data.GetList();

            foreach (var item in ProductIDs)
            {
                var ListElement = _prodcuctDetails.Productlist[item - 1];
                var ListElementPrice = _prodcuctDetails.Productlist[item - 1].Price;
                _prodcuctDetails.CartList.Add(ListElement);
                _prodcuctDetails.Totalsum += ListElementPrice;
            }

            _prodcuctDetails.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _prodcuctDetails.Email = User.FindFirstValue(ClaimTypes.Name);

            return View(_prodcuctDetails);
        }
        public IActionResult CompleteOrder()
        {
            return View();
        }

        public IActionResult RemoveCartItem(int ItemID)
        {
            var Cart = Request.Cookies.SingleOrDefault(c => c.Key == "Cart");
            string cookiestring = Cart.Value;

            int Itemindex = cookiestring.IndexOf("," + ItemID.ToString() + ",");
            int ItemIDLenght = ItemID.ToString().Length;

            if (Itemindex == -1)
            {
                if (cookiestring.StartsWith(ItemID.ToString()))
                {
                    if (cookiestring.Length > 1)
                    {
                        cookiestring = cookiestring.Remove(0, ItemIDLenght + 1);
                    }
                    else
                    {
                        cookiestring = cookiestring.Remove(0, ItemIDLenght);
                    }
                }
                else
                {
                    int DeletePosition = cookiestring.Length - (ItemIDLenght + 1);
                    cookiestring = cookiestring.Remove(DeletePosition);
                }
            }
            else
            {
                cookiestring = cookiestring.Remove(Itemindex, ItemIDLenght + 1);
            }

            Response.Cookies.Append("Cart", cookiestring);
            return RedirectToAction("ShoppingCart", "Checkout");
        }
    }
}