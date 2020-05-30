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
            var FakeOrder = CreateFakeOrderForTests();
            var FakeUserID = FakeOrder.UserID;
            var actual = _repository.GetAllOrdersByUserID(FakeUserID);

            Assert.IsType<List<Orders>>(actual);
            DeleteFakeOrderForTest(FakeOrder.OrderID);
        }
        [Fact]
        public void GetAllOrderItemsByOrderID_ShouldReturnList()
        {
            var FakeOrder = CreateFakeOrderForTests();
            var FakeID = FakeOrder.OrderID;
            var actual = _repository.GetAllOrderItemsByOrderID(FakeID);

            Assert.IsType<List<OrderItems>>(actual);
            DeleteFakeOrderForTest(FakeOrder.OrderID);
        }
        [Fact]
        public void GetOrderByOrderID_ShouldReturnOrder()
        {
            var FakeOrder = CreateFakeOrderForTests();
            var FakeID = FakeOrder.OrderID;
            var actual = _repository.GetOrderByOrderID(FakeID);

            Assert.IsType<Orders>(actual);
            DeleteFakeOrderForTest(FakeOrder.OrderID);
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
        private Orders CreateFakeOrderForTests()
        {
            Orders InsertFakeOrder = new Orders()
            { OrderID = Guid.NewGuid(), OrderMadeAt = DateTime.Now, TotalSum = 15, UserID = "FakeUserID" };
            _context.Orders.Add(InsertFakeOrder);
            _context.SaveChanges();

            return InsertFakeOrder;
        }
        private void DeleteFakeOrderForTest(Guid ID)
        {
            var DeleteProduct = _context.Orders.Where(e => e.OrderID == ID);

            Orders _Orders = new Orders();
            _Orders = DeleteProduct.Single();

            _context.Orders.Remove(_Orders);
            _context.SaveChanges();
        }
    }
}
//i product och order repotest => fixa metod som lägger in fake produkt
//fixa också en metod som raderar fakeprodukt/ordern