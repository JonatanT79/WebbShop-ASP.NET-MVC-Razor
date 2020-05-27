using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Models
{
    public class OrderItems
    {
        [Key]
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Amount { get; set; }
        public Orders Orders { get; set; }
    }
}
