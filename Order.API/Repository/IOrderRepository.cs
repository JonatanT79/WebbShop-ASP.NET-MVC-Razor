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
        List<OrderItems> GetAllOrderItemsByOrderID(Guid OrderID);
        Orders GetOrderByOrderID(Guid OrderID);
        void CreateOrder(Orders Order);
        void InsertOrderItems(List<int> Items, Guid OrdersID);
        void DeleteSingleOrderFromHistory(Guid OrderID);
        void DeleteAllUserOrders(string UserID);
        void UpdateOrder(Orders order);
    }
}
