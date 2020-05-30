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

        //List<Orders> GetAllOrders();
        //List<Orders> GetAllOrdersByUserID(string UserID);
        //List<OrderItems> GetAllOrderItemsByOrderID(Guid OrderID);
        //Orders GetOrderByOrderID(Guid OrderID);
        //void CreateOrder(Orders Order);
        //void InsertOrderItems(List<int> Items, Guid OrdersID);
        //void DeleteSingleOrderFromHistory(Guid OrderID);
        //void UpdateOrder(Orders order);

        [Fact]
        public void GetAllOrders_ShouldGetAllOrders()
        {
            var expected = _context.Orders.ToList();
            var actual = _repository.GetAllOrders();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAllOrdersByUserID_ShouldGetAllOrdersByUserID()
        {
            var expected = _context.Orders.Select(e => e.OrderID);
            var actual = _repository.GetAllOrders().Select(e => e.OrderID);

            Assert.Equal(expected, actual);
        }
    }
}
//copy from p