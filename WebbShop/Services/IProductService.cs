using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebbShop.Models;

namespace WebbShop.Services
{
    public interface IProductService
    {
        Task<List<Products>> GetAllProductsAsync();
        Task<Products> GetProductByIDAsync(int ID);
    }
}
