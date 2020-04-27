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
            _products.Name = "Gaming Dator";
            _products.Description = "Stationär";
            _products.Price = 14.999M;

            _products.Productlist.Add(_products);
            return View(_products);
        }
    }
}

//TODO lägg till orderbekräftelsesida och tillhörande knappar