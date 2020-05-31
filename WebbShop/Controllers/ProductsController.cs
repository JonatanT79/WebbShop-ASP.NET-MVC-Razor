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
}
//fixa t.ex isalingfrom och notfound
//fixa orderbyorder i orderapi