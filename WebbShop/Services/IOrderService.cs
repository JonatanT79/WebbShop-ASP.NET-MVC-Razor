using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebbShop.Models;

namespace WebbShop.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<List<OrderItems>> GetAllOrderItemsByOrderIDAsync(Guid OrderID);
        Task<List<Order>> GetAllOrdersByUserIDAsync(string UserID);
        Task InsertOrderAsync(Order order);
        Task InsertOrderItemsAsync(List<int> OrderItems, Guid OrderID);
        Task DeleteOrderAsync(Guid OrderID);
        Task DeleteAllUserOrderAsync(string UserID);
    }
}
