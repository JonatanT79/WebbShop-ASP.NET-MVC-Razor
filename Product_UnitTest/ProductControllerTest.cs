using Product.API.Data;
using Product.API.Models;
using Product.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Product_UnitTest
{
    public class ProductControllerTest
    {
        ProductRepository _productRepository = new ProductRepository();
        readonly ProductContext _context = new ProductContext();

        [Fact]
        public void ShouldGetAllProducts()
        {
            //Arrange
            List<Products> Expected = new List<Products>();
            Expected = _context.Products.ToList();

            int ExpectedTotalItems = Expected.Count();

            //act
            List<Products> Actual = new List<Products>();
            Actual = _productRepository.GetAllProducts();

            //Assert

            Assert.Equal(ExpectedTotalItems, Actual.Count());
            // Assert.Equal(Expected, Actual);
        }
    }
}
