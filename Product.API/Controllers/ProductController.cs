using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Product.API.Data;
using Product.API.Models;
using Product.API.Repository;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductRepository _productRepository = new ProductRepository();
        readonly ProductContext _context = new ProductContext();

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productRepository.GetAllProducts());
        }

        [HttpGet("{ID}")]
        public IActionResult GetProductByID(int ID)
        {
            if (_context.Products.Any(a => a.ID == ID))
            {
                return Ok(_productRepository.GetProductByID(ID));
            }
            else
            {
                return NotFound("No product have that ID");
            }
        }

        [HttpPost("Insert")]
        public IActionResult InsertNewProduct([FromBody] Products productParam)
        {
            using (var scope = new TransactionScope())
            {
                _productRepository.CreateProduct(productParam);
                scope.Complete();
                return CreatedAtAction(nameof(GetProducts), new { ID = productParam.ID }, productParam);
            }
        }

        [HttpDelete("Delete/{ID}")]
        public IActionResult DeleteProduct(int ID)
        {
            if (_context.Products.Any(a => a.ID == ID))
            {
                _productRepository.DeleteProduct(ID);
                return Ok(_productRepository.GetAllProducts());
            }
            else
            {
                return NotFound("No product have that ID");
            }
        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] Products product)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _productRepository.UpdateProduct(product);
                scope.Complete();
                return Ok();
            }
        }
    }
}
