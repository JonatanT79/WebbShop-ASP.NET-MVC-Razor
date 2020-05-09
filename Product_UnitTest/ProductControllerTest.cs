using Product.API.Data;
using Product.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Product_UnitTest
{
    public class ProductControllerTest
    {
        [Fact]
        public void ShouldGetAllProducts()
        {
            //Arrange
            List<Products> Expected;
            using (ProductContext ctx = new ProductContext())
            {
                Expected = ProductRepository.GetAllProducts();
            }

            //act
            List<Products> Actual = ProductRepository.GetAllProducts();
            //Assert
            Assert.Equal(Expected, Actual);
        }
    }
}
