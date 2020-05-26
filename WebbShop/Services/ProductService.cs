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
        private readonly HttpClient client = new HttpClient();
        public Uri BaseAdressProduct { get; set; } = new Uri("http://localhost:5000");

        public async Task<List<Products>> GetAllProductsAsync()
        {
            string ResponseString = await client.GetStringAsync(BaseAdressProduct + "product");
            var ProductList = JsonConvert.DeserializeObject<List<Products>>(ResponseString);

            return ProductList;
        }

        public async Task<Products> GetProductByIDAsync(int ID)
        {
            string ResponseString = await client.GetStringAsync(BaseAdressProduct + "product/" + ID);
            var Product = JsonConvert.DeserializeObject<Products>(ResponseString);

            return Product;
        }
    }
}
