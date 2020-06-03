using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebbShop.Models;

namespace WebbShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<UserAddress> UserAddress { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = WebShop; Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelbBuilder)
        {
            base.OnModelCreating(modelbBuilder);
            modelbBuilder.Entity<UserAddress>().HasIndex(i => i.UserID).IsUnique();
        }
    }
}
