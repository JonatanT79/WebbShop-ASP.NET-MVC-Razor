using Order.API.Data;
using Order.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Order_UnitTest
{
    public class OrderRepositoryTest
    {
        OrderRepository _repository = new OrderRepository();
        OrderContext _context = new OrderContext();
        [Fact]
        public void GetAllOrders_ShouldGetAllOrdersWithID()
        {
            var expected = _context.Orders.Select(e => e.ID);
            var actual = _repository.GetAllOrders().Select(e => e.ID);

            Assert.Equal(expected, actual);
        }
    }
}
