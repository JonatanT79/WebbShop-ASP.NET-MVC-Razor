using Product.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ProductContext _context)
        {
            if (!_context.Products.Any())
            {
                List<Products> ListOfProducts = new List<Products>()
                {
                    new Products() { ID = 1, Name = "PC", Description = "Deskop", Price = 14999f, Maker = "Acer" },
                    new Products() { ID = 2, Name = "TV", Description = "55 Tum", Price = 8999f, Maker = "Philips" },
                    new Products() { ID = 3, Name = "Headphones", Description = "Iphone", Price = 799f, Maker = "Apple"},
                    new Products() { ID = 4, Name = "Keyboard", Description = "Gaming", Price = 1050f, Maker = "Razor"}
                };

                foreach (var item in ListOfProducts)
                {
                    _context.Products.Add(item);
                }

                _context.SaveChanges();
            }
        }
    }
}
