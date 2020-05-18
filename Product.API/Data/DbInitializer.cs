using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ProductContext _context)
        {
            if(!_context.Products.Any())
            {

            }
        }
    }
}
