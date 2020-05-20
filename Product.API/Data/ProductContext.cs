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
        public DbSet<Products> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = ProductService; Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Model-Seed Database
            modelBuilder.Entity<Products>().HasData
            (
                new Products() { ID = 1, Name = "PC", Description = "Deskop", Price = 14999f, InStock = 3, Maker = "Acer" },
                new Products() { ID = 2, Name = "TV", Description = "55 Tum", Price = 8999f, InStock = 2, Maker = "Philips" },
                new Products() { ID = 3, Name = "Headphones", Description = "Iphone", Price = 799f, InStock = 5, Maker = "Apple" },
                new Products() { ID = 4, Name = "Keyboard", Description = "Gaming", Price = 1050f, InStock = 4, Maker = "Razor" }
            );
        }
    }
}
