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
        private readonly HttpClient _httpClient = new HttpClient();
        public Uri BaseAdress{ get; set; } = new Uri("http://localhost:5000");

        public async Task<List<Products>> GetAllProductsAsync()
        {
            _httpClient.DefaultRequestHeaders.Add("ReadApiKey", "SecretProductApiKey");
            string ResponseString = await _httpClient.GetStringAsync(BaseAdress + "product");
            var ProductList = JsonConvert.DeserializeObject<List<Products>>(ResponseString);

            return ProductList;
        }

        public async Task<Products> GetProductByIDAsync(int ID)
        {
            _httpClient.DefaultRequestHeaders.Add("ReadApiKey", "SecretProductApiKey");
            string ResponseString = await _httpClient.GetStringAsync(BaseAdress + "product/" + ID);
            var Product = JsonConvert.DeserializeObject<Products>(ResponseString);

            return Product;
        }
    }
}
