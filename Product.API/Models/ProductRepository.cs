using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.API.Data;

namespace Product.API.Models
{
    public class ProductRepository
    {
        public List<Products> GetAllProducts()
        {
            Products _Products = new Products();
            using (ProductContext ctx = new ProductContext())
            {
                //join productbrand
                _Products.ProductsList = ctx.Products.ToList();

                return _Products.ProductsList;
            }
        }
        public Products GetProductByID() { Products p = new Products(); return p; }
        public void CreateProduct() { }
        public void UpdateProduct() { }
        public void DeleteProduct() { }

    }
}
