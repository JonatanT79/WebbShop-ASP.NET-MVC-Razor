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

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            _httpClient.DefaultRequestHeaders.Add("ReadApiKey", "SecretOrderApiKey");
            string ResponseString = await _httpClient.GetStringAsync(BaseAdress + "order");
            var Orders = JsonConvert.DeserializeObject<List<Order>>(ResponseString);

            return Orders;
        }

        public async Task<List<OrderItems>> GetAllOrderItemsByOrderIDAsync(Guid OrderID)
        {
            _httpClient.DefaultRequestHeaders.Add("ReadApiKey", "SecretOrderApiKey");
            string ResponseString = await _httpClient.GetStringAsync(BaseAdress + "order/items/" + OrderID);
            var OrderItems = JsonConvert.DeserializeObject<List<OrderItems>>(ResponseString);

            return OrderItems;
        }

        public async Task<List<Order>> GetAllOrdersByUserIDAsync(string UserID)
        {
            _httpClient.DefaultRequestHeaders.Add("ReadApiKey", "SecretOrderApiKey");
            string ResponseString = await _httpClient.GetStringAsync(BaseAdress + "order/" + UserID);
            var AllUserOrders = JsonConvert.DeserializeObject<List<Order>>(ResponseString);

            return AllUserOrders;
        }

        public async Task InsertOrderAsync(Order order)
        {
            _httpClient.DefaultRequestHeaders.Add("ReadApiKey", "SecretOrderApiKey");
            var JsonString = JsonConvert.SerializeObject(order);
            var Content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(BaseAdress + "order/insertorder", Content);
        }

        public async Task InsertOrderItemsAsync(List<int> OrderItems, Guid OrderID)
        {
            _httpClient.DefaultRequestHeaders.Add("ReadApiKey", "SecretOrderApiKey");
            var JsonString = JsonConvert.SerializeObject(OrderItems);
            var Content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(BaseAdress + "order/insertitems/" + OrderID, Content);
        }

        public async Task DeleteOrderAsync(Guid OrderID)
        {
            _httpClient.DefaultRequestHeaders.Add("ReadApiKey", "SecretOrderApiKey");
            await _httpClient.DeleteAsync(BaseAdress + "order/delete/" + OrderID);
        }

        public async Task DeleteAllUserOrderAsync(string UserID)
        {
            _httpClient.DefaultRequestHeaders.Add("ReadApiKey", "SecretOrderApiKey");
            await _httpClient.DeleteAsync(BaseAdress + "order/delete/ByUserID/" + UserID);
        }
    }
}
