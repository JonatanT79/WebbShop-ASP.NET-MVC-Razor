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
// lägg till auth. i webbshop, orderapiController och i unitest(föreläsning)
//skriv en längre text i privacy delen

//OBS kommentera ut [ApiKeyAuth] i productcontroller för att få sidan att funka
