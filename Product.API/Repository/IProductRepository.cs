using Product.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Repository
{
    interface IProductRepository
    {
        List<Products> GetAllProducts();
        Products GetProductByID();
        void CreateProduct();
        void UpdateProduct();
        void DeleteProduct();
    }
}
