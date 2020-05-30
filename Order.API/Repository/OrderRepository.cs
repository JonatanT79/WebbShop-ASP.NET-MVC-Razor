using Order.API.Data;
using Order.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        readonly OrderContext _context = new OrderContext();

        public List<Orders> GetAllOrders()
        {
            var ListOfOrders = _context.Orders.ToList();
            return ListOfOrders;
        }
        public List<Orders> GetAllOrdersByUserID(string UserID)
        {
            var ListOfOrders = _context.Orders.Where(e => e.UserID == UserID);
            return ListOfOrders.ToList();
        }
        public List<OrderItems> GetAllOrderItemsByOrderID(Guid OrderID)
        {
            var ListOfOrderItems = _context.OrderItems.Where(e => e.OrdersID == OrderID);
            return ListOfOrderItems.ToList();
        }
        public Orders GetOrderByOrderID(Guid OrderID)
        {
            var FindOrder = _context.Orders.Where(e => e.OrderID == OrderID);
            var GetOrder = FindOrder.SingleOrDefault();

            return GetOrder;
        }
        public void CreateOrder(Orders Order)
        {
            _context.Orders.Add(Order);
            _context.SaveChanges();
        }
        public void InsertOrderItems(List<int> Items, Guid OrdersID)
        {
            foreach (var item in Items)
            {
                OrderItems _orderItems = new OrderItems() { ProductID = item, Amount = 1, OrdersID = OrdersID };
                _context.OrderItems.Add(_orderItems);
            }
            _context.SaveChanges();
        }
        public void DeleteSingleOrderFromHistory(Guid OrderID)
        {
            var GetOrderToDelete =  _context.Orders.Where(e => e.OrderID == OrderID);
            var Order = GetOrderToDelete.SingleOrDefault();
            _context.Orders.Remove(Order);
            _context.SaveChanges();
        }

        public void UpdateOrder(Orders order)
        {
            var Getorder = (from e in _context.Orders
                             where e.OrderID == order.OrderID
                             select e).SingleOrDefault();

           Getorder.OrderMadeAt = order.OrderMadeAt;
           Getorder.TotalSum = order.TotalSum;
           Getorder.UserID = order.UserID;
        }
    }
}
