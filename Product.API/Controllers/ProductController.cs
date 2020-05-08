using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Product.API.Models;

namespace Product.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        Products _Products = new Products();

        [HttpGet]
        public ActionResult GetProducts()
        {
            _Products.ProductsList = Repository.GetAllProducts();

            return Ok(new { _Products.ProductsList});
        }
    }
}