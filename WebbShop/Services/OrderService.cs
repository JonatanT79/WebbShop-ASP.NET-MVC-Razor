using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebbShop.Models;

namespace WebbShop.Services
{
    public class OrderService :IOrderService
    {
        HttpClient _httpClient = new HttpClient();
        public Uri BaseAdress { get; set; } = new Uri("http://localhost:5000/");

        //Obs endast för test, kommer någ inte behövas hämta alla ordrar
        public async Task<List<Order>> GetAllOrders()
        {
            string ResponseString = await _httpClient.GetStringAsync(BaseAdress + "order");
            var Orders = JsonConvert.DeserializeObject<List<Order>>(ResponseString);
            return Orders;
        }
    }
}
