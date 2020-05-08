using System;
using System.Collections.Generic;
using System.Linq;
using WebbShop.Controllers;
using WebbShop.Models;
using Xunit;

namespace Product_UnitTest
{
    public class GetProductsTest
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            List<Products> list = Data.GetList();
            var FilterList = from e in list
                         //  where e.ID != 3
                           select e;

            var expectedList = FilterList.ToList();

            //act
            var actual = list;
            //Assert
            Products p = new Products();
            OrderViewModel vm = new OrderViewModel();
            Assert.IsType<List<Products>>(list);
           // Assert.Equal(expectedList, actual);
        }
    }
}
