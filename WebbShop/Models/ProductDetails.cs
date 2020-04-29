using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebbShop.Models
{
    public class ProductDetails
    {
        public int ID { get; set; }
        public DateTime Release { get; set; }
        public decimal Totalsum { get; set; }
        public string UserID { get; set; }
        public string Email { get; set; }

        public List<Products> Productlist = new List<Products>();

        public List<Products> CartList = new List<Products>();
    }
}
