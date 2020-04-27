using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebbShop.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult IndexProducts()
        {
            return View();
        }
    }
}