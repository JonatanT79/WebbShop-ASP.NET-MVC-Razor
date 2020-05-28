using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebbShop.Models;

namespace WebbShop.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetAllOrdersByUserID(string UserID);
        Task InsertOrder(Order order);
        Task InsertOrderItems(List<int> OrderItems, Guid OrderID);
    }
}
