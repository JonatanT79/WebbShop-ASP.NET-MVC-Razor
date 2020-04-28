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
            _productdetails.Productlist = GetList();

            return View(_productdetails);
        }
        public IActionResult ViewProduct(int ID)
        {
            //Test data, OBS!, Koden ska skrivas om senare när databasen finns ********************************************
         
            _productdetails.ID = ID;
            _productdetails.Release = DateTime.Now;
            _productdetails.Productlist = GetList();

            return View(_productdetails);
        }
        public List<Products> GetList()
        {
            _productdetails.Productlist = new List<Products>()
            {
                new Products() { ID = 1, Name = "Dator", Description = "Stationär", Price = 14999M, Maker = "Acer" },
                new Products() { ID = 2, Name = "TV", Description = "55 Tum", Price = 8999M, Maker = "Philips" },
                new Products() { ID = 3, Name = "Hörlurar", Description = "Iphone", Price = 799M, Maker = "Apple"},
            };
            return _productdetails.Productlist;
        }
    }
}
//Modifiering varukorg
//Lagerstatus
//översätt bilder till engelska
//TODO (senare) lägg till admin sida för att skapa/editera/ta bort produkter
//TODO responsivitet (senare)
//Lägg till Get/post för mer tydlighet
//fixa så cookiestringen går ut efter 60 min