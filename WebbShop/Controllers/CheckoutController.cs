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
    public class CheckoutController : Controller
    {
        ProductService _productService = new ProductService();

        [HttpGet]
        public async Task<IActionResult> ShoppingCart()
        {
            OrderViewModel _ViewModel = new OrderViewModel();
            var Cart = Request.Cookies.SingleOrDefault(k => k.Key == "Cart");
            string ReadKeyValue = Cart.Value;

            if (!string.IsNullOrEmpty(ReadKeyValue))
            {
                var ProductIds = ReadKeyValue.Split(",").Select(s => int.Parse(s));
                var ProductList = await _productService.GetAllProductsAsync();

                var GetProducts = from e in ProductList
                                  where ProductIds.Contains(e.ID)
                                  select e;

                foreach (var item in GetProducts)
                {
                    _ViewModel.CartList.Add(item);
                    _ViewModel.Totalsum += item.Price * _ViewModel.Amount;
                }
            }

            return View(_ViewModel);
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
        public IActionResult AddItemToCart(int ID)
        {
            OrderViewModel _ViewModel = new OrderViewModel();
            var Cart = Request.Cookies.SingleOrDefault(c => c.Key == "Cart");
            string cookiestring = Cart.Value + "";

            if (!string.IsNullOrEmpty(Cart.Value))
            {
                cookiestring = Cart.Value + "," + ID;
            }
            else
            {
                cookiestring = ID.ToString();
            }

            Response.Cookies.Append("Cart", cookiestring);

            return RedirectToAction("ShoppingCart", "Checkout");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmOrder()
        {
            OrderViewModel ViewModel = new OrderViewModel();
            var Cart = Request.Cookies.SingleOrDefault(c => c.Key == "Cart");
            string CookieValue = Cart.Value;

            var ProductIDs = CookieValue.Split(",").Select(s => int.Parse(s));
            var Productlist = await _productService.GetAllProductsAsync();

            var Getproducts = from g in Productlist
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