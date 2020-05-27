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
        private readonly HttpClient _httpClient = new HttpClient();
        public Uri BaseAdress { get; set; } = new Uri("http://localhost:5000/");

        public async Task<List<Order>> GetAllOrders()
        {
            string ResponseString = await _httpClient.GetStringAsync(BaseAdress + "order");
            var Orders = JsonConvert.DeserializeObject<List<Order>>(ResponseString);
            return Orders;
        }

        public async Task<List<Order>> GetAllOrdersByUserID(string UserID)
        {
            string ResponseString = await _httpClient.GetStringAsync(BaseAdress + "order/" + UserID);
            var AllUserOrders = JsonConvert.DeserializeObject<List<Order>>(ResponseString);

            return AllUserOrders;
        }
    }
}
