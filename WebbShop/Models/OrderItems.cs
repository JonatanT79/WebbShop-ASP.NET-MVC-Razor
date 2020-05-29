using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebbShop.Models
{
    public class OrderItems
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }
        public Guid OrdersID { get; set; }
    }
}
