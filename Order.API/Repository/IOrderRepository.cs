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
        Orders GetOrderByID(Guid ID);
    }
}
