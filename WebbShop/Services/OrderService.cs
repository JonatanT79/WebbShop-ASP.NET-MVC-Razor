using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebbShop.Models;

namespace WebbShop.Services
{
    public class OrderService : IOrderService
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

        public async Task InsertOrder(Order order)
        {
            var JsonString = JsonConvert.SerializeObject(order);
            var Content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(BaseAdress + "order/insertorder", Content);
        }

        public async Task InsertOrderItems(List<int> OrderItems, Guid OrderID)
        {
            var JsonString = JsonConvert.SerializeObject(OrderItems);
            var Content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(BaseAdress + "order/insertitems/" + OrderID, Content);
        }
    }
}
