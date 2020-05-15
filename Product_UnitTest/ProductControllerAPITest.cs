using Microsoft.AspNetCore.Mvc;
using Product.API.Controllers;
using Product.API.Data;
using Product.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Product_UnitTest
{
    public class ProductControllerAPITest
    {
        ProductAPIController _controller = new ProductAPIController();
        ProductContext _context = new ProductContext();
        [Fact]
        public void GetProductsShouldBeOk()
        {
            var actual = _controller.GetProducts();
            Assert.IsType<OkObjectResult>(actual);
        }
        [Fact]
        public void GetProductsShouldReturnProductList()
        {
            var actual = _controller.GetProducts();
            Assert.IsAssignableFrom<List<Products>>((actual as OkObjectResult).Value);
        }
    }
}
