using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Product.API.Models;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        Products _Products = new Products();
        ProductRepository _ProductRepository = new ProductRepository();

        [HttpGet]
        public ActionResult GetProducts()
        {
            _Products.ProductsList = _ProductRepository.GetAllProducts();
            return Ok(new { _Products.ProductsList});
        }
    }
}
//TODO: skriv test för GetProducts