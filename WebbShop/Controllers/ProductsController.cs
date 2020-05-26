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
        ProductService _productService = new ProductService();
        OrderViewModel _ViewModel = new OrderViewModel();

        [HttpGet]
        public async Task<IActionResult> IndexProducts()
        {
            // Get the list of values
            //  _ViewModel.Productlist = Data.GetList();
            _ViewModel.Productlist = await _productService.GetAllProducts();

            return View(_ViewModel);
        }

        [HttpGet]
        public IActionResult ViewProduct(int ID)
        {
            _ViewModel.Release = DateTime.Now;
            _ViewModel.Productlist = Data.GetList();

            var Filterproduct = from e in _ViewModel.Productlist
                                where e.ID == ID
                                select e;

            var Product = Filterproduct.ToList()[0];
            _ViewModel.CartList.Add(Product);

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
//skapa en klass (services) som hämtar data från apigatewayen med http client. Klassen ska anropas här
// serviceklassen ska anropas för respektive controller