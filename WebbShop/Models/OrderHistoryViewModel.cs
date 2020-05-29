using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebbShop.Models
{
    public class OrderHistoryViewModel
    {
        public List<Order> OrderList = new List<Order>();

        public List<OrderItems> OrderItemsList = new List<OrderItems>();
    }
}
