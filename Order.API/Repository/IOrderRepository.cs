using Order.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Repository
{
    interface IOrderRepository
    {
        List<Orders> GetAllOrders();
        List<Orders> GetAllOrdersByUserID(string UserID);
        Orders GetOrderByOrderID(Guid OrderID);
        void CreateOrder(Orders Order);
        void InsertOrderItems(List<int> Items, Guid OrdersID);
        void DeleteSingleOrderFromHistory(Guid OrderID);
    }
}
