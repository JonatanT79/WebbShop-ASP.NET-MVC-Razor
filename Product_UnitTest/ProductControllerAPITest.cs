using Microsoft.AspNetCore.Mvc;
using Product.API;
using Product.API.Controllers;
using Product.API.Data;
using Product.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Product_UnitTest
{
    public class ProductControllerAPITest
    {
        ProductController _controller = new ProductController();
        ProductContext _context = new ProductContext();
        private HttpClient Client;

        public ProductControllerAPITest(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }
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

        [Fact]
        public void InsertNewProduct_ShouldReturnCreate()
        {
            Products FakeProduct = new Products() { Name = "Fake", Description = "Fake", Price = 15, InStock = 1, Maker = "FakeMaker"};
            var actualActionType = _controller.InsertNewProduct(FakeProduct);

            var FindFakeProduct = _context.Products.Where(e => e.Name == "Fake");
            Products RemoveFakeProduct = FindFakeProduct.First();
            _context.Products.Remove(RemoveFakeProduct);
            _context.SaveChanges();
            // sätt i en separat metod
            Assert.IsAssignableFrom<Products>((actualActionType as CreatedAtActionResult).Value);
            Assert.IsType<CreatedAtActionResult>(actualActionType);
        }
    }
}
//använd hemsida testkoden