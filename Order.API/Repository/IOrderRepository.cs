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
        Orders GetOrderByID(Guid ID);
    }
}
