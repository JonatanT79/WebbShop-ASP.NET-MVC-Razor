using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Product.API.Models;
using Product.API.Repository;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        ProductRepository _productRepository = new ProductRepository();

        [HttpGet]
        public ActionResult GetProducts()
        {
            return Ok(new { ProductsList = _productRepository.GetAllProducts() });
        }

        [HttpGet("{ID}")]
        public Products GetProductByID(int ID)
        {
            return _productRepository.GetProductByID(ID);
        }
    }
}
