using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.API.Data;

namespace Product.API.Models
{
    public class Repository
    {
        public static List<Products> GetAllProducts()
        {
            Products _Products = new Products();
            using (ProductContext ctx = new ProductContext())
            {
                var GetProducts = from e in ctx.Products
                                  select e;

                foreach (var item in GetProducts)
                {
                    _Products.ProductsList.Add(item);
                }

                return _Products.ProductsList;
            }
        }
    }
}
