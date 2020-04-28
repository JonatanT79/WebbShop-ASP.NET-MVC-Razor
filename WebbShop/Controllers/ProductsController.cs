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
        public IActionResult IndexProducts()
        {
            Products _products = new Products();
            _products.Productlist = new List<Products>()
            {
                new Products() { ID = 1, Name = "Dator", Description = "Stationär", Price = 14999M },
                new Products() { ID = 2, Name = "TV", Description = "55 Tum", Price = 8999M },
                new Products() { ID = 3, Name = "Hörlurar", Description = "Iphone", Price = 799M },
            };

            return View(_products);
        }
        public IActionResult ViewProduct(int ID)
        {
            //Test data, OBS ifsatsen ska bytas ut mot LINQ senare när databasen finns
            ProductDetails _productdetails = new ProductDetails();
            if(ID == 1)
            {
                _productdetails.ID = 1;
                _productdetails.Name = "Dator";
                _productdetails.Description = "Stationär";
                _productdetails.Price = 14999M;
                var DateAndTime = DateTime.Now;
                _productdetails.Release = DateAndTime.Date;
                _productdetails.Maker = "MSI";
            }
            else if(ID == 2)
            {
                _productdetails.ID = 2;
                _productdetails.Name = "TV";
                _productdetails.Description = "55 Tum";
                _productdetails.Price = 8999M;
                var DateAndTime = DateTime.Now;
                _productdetails.Release = DateAndTime.Date;
                _productdetails.Maker = "Philips";
            }
            else if(ID == 3)
            {
                _productdetails.ID = 3;
                _productdetails.Name = "Hörlurar";
                _productdetails.Description = "Iphone";
                _productdetails.Price = 799M;
                var DateAndTime = DateTime.Now;
                _productdetails.Release = DateAndTime.Date;
                _productdetails.Maker = "Apple";
            }

            return View(_productdetails);
        }
    }
}

//Lagerstatus
//översätt bilder till engelska
//TODO (senare) lägg till admin sida för att skapa/editera/ta bort produkter
//TODO responsivitet