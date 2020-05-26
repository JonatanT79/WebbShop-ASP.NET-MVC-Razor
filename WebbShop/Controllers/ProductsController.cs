using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebbShop.Models;
using WebbShop.Services;

namespace WebbShop.Controllers
{
    public class ProductsController : Controller
    {
        OrderViewModel _ViewModel = new OrderViewModel();
        private readonly ProductService _productService = new ProductService();

        [HttpGet]
        public async Task<IActionResult> IndexProducts()
        {
            _ViewModel.Productlist = await _productService.GetAllProductsAsync();

            return View(_ViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ViewProduct(int ID)
        {
            _ViewModel.Release = DateTime.Now;
            var product = await _productService.GetProductByIDAsync(ID);
            _ViewModel.CartList.Add(product);

            return View(_ViewModel);
        }
    }
    public class Data
    {
        public static List<Products> GetList()
        {
            List<Products> ListOfProducts = new List<Products>()
            {
                new Products() { ID = 1, Name = "PC", Description = "Deskop", Price = 14999M, Maker = "Acer" },
                new Products() { ID = 2, Name = "TV", Description = "55 Tum", Price = 8999M, Maker = "Philips" },
                new Products() { ID = 3, Name = "Headphones", Description = "Iphone", Price = 799M, Maker = "Apple"},
                new Products() { ID = 4, Name = "Keyboard", Description = "Gaming", Price = 1050M, Maker = "Razor"}
            };

            return ListOfProducts;
        }
    }
}
