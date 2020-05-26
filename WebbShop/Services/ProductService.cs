using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebbShop.Models;

namespace WebbShop.Services
{
    public class ProductService : IProductService
    {
        readonly HttpClient client;
        public async Task<List<Products>> GetAllProducts()
        {
            var ResponseString = await client.GetStringAsync("product/");
            
            var ProductList = JsonConvert.DeserializeObject<List<Products>>(ResponseString);

            return ProductList;
        }

        public async Task<Products> GetProductByID()
        {
            var ResponseString = await client.GetStringAsync("k");

            var Product = JsonConvert.DeserializeObject<Products>(ResponseString);

            return Product;
        }
    }
}
