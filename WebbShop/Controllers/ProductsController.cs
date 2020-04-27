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

            Products _products1 = new Products();
            _products1.ID = 1;
            _products1.Name = "Gaming Dator";
            _products1.Description = "Stationär";
            _products1.Price = 14999M;

            Products _products2 = new Products();
            _products2.ID = 2;
            _products2.Name = "TV";
            _products2.Description = "55 Tum";
            _products2.Price = 8999M;

            Products _products3 = new Products();
            _products3.ID = 3;
            _products3.Name = "Hörlurar";
            _products3.Description = "Iphone";
            _products3.Price = 799M;

            _products.Productlist.Add(_products1);
            _products.Productlist.Add(_products2);
            _products.Productlist.Add(_products3);

            return View(_products);
        }
        public IActionResult ViewProduct(int ID)
        {
            //Test data, OBS ifsatsen ska bytas ut mot LINQ senare när databasen finns
            ProductDetails _productdetails = new ProductDetails();
            if(ID == 1)
            {
                _productdetails.Name = "Dator";
                _productdetails.Description = "Stationär";
                _productdetails.Price = 14999M;
                var DateAndTime = DateTime.Now;
                _productdetails.Release = DateAndTime.Date;
                _productdetails.Maker = "MSI";
            }
            else if(ID == 2)
            {
                _productdetails.Name = "TV";
                _productdetails.Description = "55 Tum";
                _productdetails.Price = 8999M;
                var DateAndTime = DateTime.Now;
                _productdetails.Release = DateAndTime.Date;
                _productdetails.Maker = "Philips";
            }
            else if(ID == 3)
            {
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

//TODO lägg till orderbekräftelsesida och tillhörande knappar
//TODO (senare) lägg till admin sida för att skapa/editera/ta bort produkter
//Lagerstatus