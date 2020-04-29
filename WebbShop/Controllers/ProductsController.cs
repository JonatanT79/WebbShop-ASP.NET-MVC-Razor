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
            //Test data, OBS!, Koden ska skrivas om senare när databasen finns ********************************************

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
                new Products() { ID = 1, Name = "PC", Description = "Stationär", Price = 14999M, Maker = "Acer" },
                new Products() { ID = 2, Name = "TV", Description = "55 Tum", Price = 8999M, Maker = "Philips" },
                new Products() { ID = 3, Name = "Headphones", Description = "Iphone", Price = 799M, Maker = "Apple"},
            };
            return _productdetails.Productlist;
        }
    }
}
//Modifiering varukorg
//Lagerstatus
//TODO (senare) lägg till admin sida för att skapa/editera/ta bort produkter
//TODO responsivitet (senare)
//Lägg till Get/post för mer tydlighet (slutet av labb1)
//Uppgradera (passive)
