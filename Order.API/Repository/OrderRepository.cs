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
        public Orders GetOrderByID(Guid OrderID)
        {
            var FindOrder = _context.Orders.Where(e => e.OrderID == OrderID);
            var GetOrder = FindOrder.Single();

            return GetOrder;
        }
    }
}
