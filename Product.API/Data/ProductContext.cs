using Microsoft.EntityFrameworkCore;
using Product.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Data
{
    public class ProductContext : DbContext
    {
        DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = ProductService; Trusted_Connection = True;");
        }
    }
}
