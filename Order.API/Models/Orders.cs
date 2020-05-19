using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Models
{
    public class Orders
    {
        [Key]
        public Guid ID { get; set; }
        public int ProductID { get; set; }
        public string UserID { get; set; }
    }
}
