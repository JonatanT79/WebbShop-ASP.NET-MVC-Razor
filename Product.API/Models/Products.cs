﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Models
{
    public class Products
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public float Price { get; set; }
        public int InStock { get; set; }
        public ProductBrand ProductBrand { get; set; }

        public List<Products> ProductsList = new List<Products>();
    }
}