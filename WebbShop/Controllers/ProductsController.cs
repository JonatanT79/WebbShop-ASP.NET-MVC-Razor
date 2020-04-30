using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebbShop.Models;

namespace WebbShop.Controllers
{
    public class ProductsController : Controller
    {
        ProductDetails _productdetails = new ProductDetails();
        public IActionResult IndexProducts()
        {
            // Get the list of values
            _productdetails.Productlist = Data.GetList();

            return View(_productdetails);
        }
        public IActionResult ViewProduct(int ID)
        {
            _productdetails.ID = ID;
            _productdetails.Release = DateTime.Now;
            _productdetails.Productlist = Data.GetList();

            return View(_productdetails);
        }
    }
    class Data
    {
        public static List<Products> GetList()
        {
            ProductDetails _productdetails = new ProductDetails();

            _productdetails.Productlist = new List<Products>()
            {
                new Products() { ID = 1, Name = "PC", Description = "Deskop", Price = 14999M, Maker = "Acer" },
                new Products() { ID = 2, Name = "TV", Description = "55 Tum", Price = 8999M, Maker = "Philips" },
                new Products() { ID = 3, Name = "Headphones", Description = "Iphone", Price = 799M, Maker = "Apple"},
                new Products() { ID = 4, Name = "Keyboard", Description = "Gaming", Price = 1050M, Maker = "Razor"}
            };
            return _productdetails.Productlist;
        }
    }
}
//Remove
//Vid refresh läggs en ny produkt till -fixa det
//CSS
//Modifiering varukorg (+-)
//TODO (senare) lägg till admin sida för att skapa/editera/ta bort produkter
//TODO responsivitet (senare)
//Lägg till Get/post för mer tydlighet (slutet av labb1)
//Uppgradera (passive)
