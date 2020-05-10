using Product.API.Data;
using Product.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        readonly ProductContext _context = new ProductContext();
        public List<Products> GetAllProducts()
        {
            var query = from e in _context.Products
                        select e;

            return query.ToList();
        }
        public Products GetProductByID() { Products p = new Products(); return p; }
        public void CreateProduct() { }
        public void UpdateProduct() { }
        public void DeleteProduct() { }

    }
}
