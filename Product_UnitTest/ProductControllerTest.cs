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
            var expected = from e in _context.Products
                           select e.ID + " " + e.Name;

            int ExpectedTotalItems = expected.Count();

            //act
            var actual = from e in _productRepository.GetAllProducts()
                         select e.ID + " " + e.Name;

            //Assert
            Assert.Equal(ExpectedTotalItems, actual.Count());
            Assert.IsType<List<Products>>(_productRepository.GetAllProducts());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldGetProductByID()
        {
            int TestID = 2;
            Assert.NotNull(_productRepository.GetProductByID(TestID));
            Assert.IsType<Products>(_productRepository.GetProductByID(TestID));
        }
    }
}
