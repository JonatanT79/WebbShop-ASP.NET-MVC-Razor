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
            List<Products> Expected = new List<Products>();

            using (ProductContext ctx = new ProductContext())
            {
                Expected = ctx.Products.ToList();
            }

            int ExpectedTotalItems = Expected.Count();

            //act
            List<Products> Actual = new List<Products>();
            Actual = _ProductRepository.GetAllProducts();

            //Assert

              Assert.Equal(ExpectedTotalItems, Actual.Count());
             // Assert.Equal(Expected, Actual);
        }
    }
}
