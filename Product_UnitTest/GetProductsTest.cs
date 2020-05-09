using Product.API.Data;
using Product.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Product_UnitTest
{
    public class GetProductsTest
    {
        [Fact]
        public void ShouldGetAllProducts()
        {
            //Arrange
            List<Products> Expected;
            using (ProductContext ctx = new ProductContext())
            {
                Expected = Repository.GetAllProducts();
            }

            //act
            List<Products> Actual = Repository.GetAllProducts();
            //Assert
            Assert.Equal(Expected, Actual);
        }
    }
}
