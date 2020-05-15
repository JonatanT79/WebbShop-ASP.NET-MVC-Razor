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
        public void GetProducts_ShouldReturnOk()
        {
            var actual = _controller.GetProducts();
            Assert.IsType<OkObjectResult>(actual);
        }
        [Fact]
        public void GetProducts_ShouldReturnProductList()
        {
            var actual = _controller.GetProducts();
            Assert.IsAssignableFrom<List<Products>>((actual as OkObjectResult).Value);
        }

        [Fact]
        public void GetProductByID_1_ShouldReturnOk()
        {
            var actual = _controller.GetProductByID(1);
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public void GetProductByID_0_ShouldReturnNotFound()
        {
            var actual = _controller.GetProductByID(0);
            Assert.IsType<NotFoundObjectResult>(actual);
        }
    }
}
