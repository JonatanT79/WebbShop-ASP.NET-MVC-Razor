using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebbShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //ändra databasnamnet
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = TranbarDB; Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelbBuilder)
        {
            base.OnModelCreating(modelbBuilder);
        }
    }
}
