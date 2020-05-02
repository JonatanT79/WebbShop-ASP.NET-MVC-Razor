﻿using System;
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
            OrderViewModel ViewModel = new OrderViewModel();

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
            ViewModel.Productlist = Data.GetList();

            if (!string.IsNullOrEmpty(cookiestring))
            {
                var productIds = cookiestring.Split(",").Select(c => int.Parse(c));

                var Getproducts = from e in ViewModel.Productlist
                                  where productIds.Contains(e.ID)
                                  select e;

                foreach (var item in Getproducts)
                {
                    ViewModel.CartList.Add(item);
                    ViewModel.Totalsum += (item.Price * ViewModel.Amount);
                }
            }

            return View(ViewModel);
        }
        [HttpPost]
        public IActionResult ShoppingCart(bool Isempty)
        {
            OrderViewModel ViewModel = new OrderViewModel();

            if (Isempty == true)
            {
                ViewModel.StatusMessage = "Cannot continue, your cart is empty!";
            }
            else
            {
                Response.Cookies.Delete("Cart");

            }
            return View(ViewModel);
        }
        [HttpGet]
        public IActionResult ConfirmOrder()
        {
            OrderViewModel ViewModel = new OrderViewModel();
            var Cart = Request.Cookies.SingleOrDefault(c => c.Key == "Cart");
            string CookieValue = Cart.Value;

            var ProductIDs = CookieValue.Split(",").Select(s => int.Parse(s));
            ViewModel.Productlist = Data.GetList();

            var Getproducts = from g in ViewModel.Productlist
                              where ProductIDs.Contains(g.ID)
                              select g;

            foreach (var item in Getproducts)
            {
                ViewModel.CartList.Add(item);
                ViewModel.Totalsum += (item.Price * ViewModel.Amount);
            }

            ViewModel.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewModel.Email = User.FindFirstValue(ClaimTypes.Name);

            return View(ViewModel);
        }
        [HttpGet]
        public IActionResult CompleteOrder()
        {
            return View();
        }
        [HttpGet]
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
                    if (cookiestring.Contains(","))
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