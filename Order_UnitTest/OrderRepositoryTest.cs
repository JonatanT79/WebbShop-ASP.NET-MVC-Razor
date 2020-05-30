using Order.API.Data;
using Order.API.Models;
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
        public void GetAllOrders_ShouldReturnList()
        {
            var actual = _repository.GetAllOrders();
            Assert.IsType<List<Orders>>(actual);
        }

        [Fact]
        public void GetAllOrdersByUserID_ShouldReturnList()
        {
            var FakeID = "TestID";
            var actual = _repository.GetAllOrdersByUserID(FakeID);

            Assert.IsType<List<Orders>>(actual);
        }
        [Fact]
        public void GetAllOrderItemsByOrderID_ShouldReturnList()
        {
            var FakeID = Guid.NewGuid();
            var actual = _repository.GetAllOrderItemsByOrderID(FakeID);

            Assert.IsType<List<OrderItems>>(actual);

        }
        [Fact]
        public void GetOrderByOrderID_ShouldReturnOrder()
        {
            var FakeID = Guid.NewGuid();
            var expected = _context.Orders.Where(e => e.OrderID == FakeID).SingleOrDefault();
            var actual = _repository.GetOrderByOrderID(FakeID);

            Assert.Equal(expected, actual);
        }
        // fixa *****************************
        [Fact]
        public void CreateOrder_ShouldReturnList()
        {
            var expected = _context.Orders.Select(e => e.UserID).ToList();
            var actual = _repository.GetAllOrders().Select(e => e.UserID).ToList();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void InsertOrderItems_ShouldReturnList()
        {
            var expected = _context.Orders.Select(e => e.UserID).ToList();
            var actual = _repository.GetAllOrders().Select(e => e.UserID).ToList();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void DeleteSingleOrderFromHistory_ShouldReturnList()
        {
            var expected = _context.Orders.Select(e => e.UserID).ToList();
            var actual = _repository.GetAllOrders().Select(e => e.UserID).ToList();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void UpdateOrder_ShouldReturnList()
        {
            var expected = _context.Orders.Select(e => e.UserID).ToList();
            var actual = _repository.GetAllOrders().Select(e => e.UserID).ToList();

            Assert.Equal(expected, actual);
        }
    }
}
//i product och order repotest => fixa metod som lägger in fake produkt
//fixa också en metod som raderar fakeprodukt/ordern