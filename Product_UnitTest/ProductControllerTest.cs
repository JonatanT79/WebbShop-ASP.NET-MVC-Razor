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
        ProductRepository _ProductRepository = new ProductRepository();
        [Fact]
        public void ShouldGetAllProducts()
        {
            //Arrange
            List<Products> Expected;
            using (ProductContext ctx = new ProductContext())
            {
                Expected = _ProductRepository.GetAllProducts();
            }

            //act
            List<Products> Actual = _ProductRepository.GetAllProducts();
            //Assert
            Assert.Equal(Expected, Actual);
        }
    }
}
