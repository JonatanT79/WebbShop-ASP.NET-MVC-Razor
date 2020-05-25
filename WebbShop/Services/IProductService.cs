using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebbShop.Models;

namespace WebbShop.Services
{
    interface IProductService
    {
        Task<List<Products>> GetAllProducts();
        Task<Products> GetProductByID();
    }
}
