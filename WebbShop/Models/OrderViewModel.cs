using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebbShop.Models
{
    public class OrderViewModel
    {
        public int ID { get; set; }
        public DateTime Release { get; set; }
        public decimal Totalsum { get; set; }
        public int Amount { get; set; } = 1;
        public string UserID { get; set; }
        public string Email { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

        public List<Products> Productlist = new List<Products>();

        public List<Products> CartList = new List<Products>();
    }
}
