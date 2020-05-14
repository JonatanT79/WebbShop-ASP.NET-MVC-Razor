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
        ProductContext _context = new ProductContext();
        [Fact]
        public void GetProductsShouldBeOk()
        {
            ProductAPIController _controller = new ProductAPIController();
            var actual = _controller.GetProducts();
            Assert.IsType<OkObjectResult>(actual);
        }
        [Fact]
        public void GetProductsShouldReturnProductList()
        {
            ProductAPIController _controller = new ProductAPIController();
            var actual = _controller.GetProducts();
            //fixa
            Assert.IsAssignableFrom<List<Products>>((actual as OkObjectResult).Value);
        }
    }
}
