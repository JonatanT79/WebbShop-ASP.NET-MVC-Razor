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
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Release { get; set; }
        public string Maker { get; set; }
        public decimal Totalsum { get; set; }

        public List<Products> Productlist = new List<Products>();

        public List<Products> CartList = new List<Products>();
    }
}
