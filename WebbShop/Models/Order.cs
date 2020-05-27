using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebbShop.Models
{
    public class Order
    {
        public Guid OrderID { get; set; }
        public float TotalSum { get; set; }
        public DateTime OrderMadeAt { get; set; }
        public string UserID { get; set; }
    }
}
