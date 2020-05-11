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
        Products GetProductByID(int ID);
        void CreateProduct(Products product);
        void DeleteProduct(int ID);
        void UpdateProduct(Products product);
    }
}
