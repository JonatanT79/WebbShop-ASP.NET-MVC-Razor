using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Order.API;
using Order.API.Controllers;
using Order.API.Data;
using Order.API.Models;
using Order.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Order_UnitTest
{
    public class OrderControllerAPITest : IClassFixture<TestFixture<Startup>>
    {
        OrderRepository _repository = new OrderRepository();
        OrderContext _context = new OrderContext();
        private HttpClient Client;

        public OrderControllerAPITest(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async void GetOrders_ShouldReturnOK()
        {
            string request = "/api/order";
            var response = await Client.GetAsync(request);
            var actual = response.StatusCode;
            Assert.Equal(HttpStatusCode.OK, actual);
        }
        [Fact]
        public async void GetOrders_ShouldReturnList()
        {
            string request = "/api/order";
            var response = await Client.GetAsync(request);

            string ResponseString = await response.Content.ReadAsStringAsync();
            var Content = JsonConvert.DeserializeObject<List<Orders>>(ResponseString);

            Assert.IsType<List<Orders>>(Content);
        }

        [Fact]
        public async void GetAllOrderItems_ShouldReturnOK()
        {
            var FakeOrder = CreateFakeOrderForTests();
            string request = "/api/order/items/" + FakeOrder.OrderID;
            var response = await Client.GetAsync(request);
            var actual = response.StatusCode;
            DeleteFakeOrderForTest(FakeOrder.OrderID);

            Assert.Equal(HttpStatusCode.OK, actual);
        }
        [Fact]
        public async void GetAllOrderItems_ShouldReturnList()
        {
            var FakeOrder = CreateFakeOrderForTests();
            string request = "/api/order/items/" + FakeOrder.OrderID;
            var response = await Client.GetAsync(request);

            string ResponseString = await response.Content.ReadAsStringAsync();
            var Content = JsonConvert.DeserializeObject<List<Orders>>(ResponseString);
            DeleteFakeOrderForTest(FakeOrder.OrderID);

            Assert.IsType<List<Orders>>(Content);
        }

        [Fact]
        public async void GetAllOrdersByUserID_ShouldReturnOK()
        {
            var FakeOrder = CreateFakeOrderForTests();
            string request = "/api/order/" + FakeOrder.UserID;
            var response = await Client.GetAsync(request);
            var actual = response.StatusCode;
            DeleteFakeOrderForTest(FakeOrder.OrderID);

            Assert.Equal(HttpStatusCode.OK, actual);
        }

        [Fact]
        public async void GetAllOrdersByUserID_ShouldReturnList()
        {
            var FakeOrder = CreateFakeOrderForTests();
            string request = "/api/order/" + FakeOrder.UserID;
            var response = await Client.GetAsync(request);

            string ResponseString = await response.Content.ReadAsStringAsync();
            var Content = JsonConvert.DeserializeObject<List<Orders>>(ResponseString);
            DeleteFakeOrderForTest(FakeOrder.OrderID);

            Assert.IsType<List<Orders>>(Content);
        }

        [Fact]
        public async void GetOrderByOrderID_IdDoesExists_ShouldReturnOK()
        {
            var FakeOrder = CreateFakeOrderForTests();
            string request = "/api/order/single/" + FakeOrder.OrderID;
            var response = await Client.GetAsync(request);
            var actual = response.StatusCode;
            DeleteFakeOrderForTest(FakeOrder.OrderID);

            Assert.Equal(HttpStatusCode.OK, actual);
        }

        [Fact]
        public async void GetOrderByOrderID_IdDoesNotExists_ShouldReturnNotFound()
        {
            Guid FakeGuid = Guid.NewGuid();
            string request = "/api/order/single/" + FakeGuid;
            var response = await Client.GetAsync(request);
            var actual = response.StatusCode;

            Assert.Equal(HttpStatusCode.NotFound, actual);
        }

        [Fact]
        public async void CreateOrder_ShouldReturnCreatedAtAction()
        {
            var FakeOrder = new Orders()
            { OrderID = Guid.NewGuid(), OrderMadeAt = DateTime.Now, TotalSum = 15, UserID = "FakeUserID" };
            var JsonString = JsonConvert.SerializeObject(FakeOrder);
            var Content = new StringContent(JsonString, Encoding.UTF8, "application/json");

            string request = "/api/order/insertorder";
            var response = await Client.PostAsync(request, Content);
            var actual = response.StatusCode;
            DeleteFakeOrderForTest(FakeOrder.OrderID);

            Assert.Equal(HttpStatusCode.Created, actual);
        }

        [Fact]
        public async void AddOrderItems_ShouldReturnCreatedAtAction()
        {
            var FakeOrder = CreateFakeOrderForTests();
            List<int> FakeList = new List<int>() { 15 };
            var JsonString = JsonConvert.SerializeObject(FakeList);
            var Content = new StringContent(JsonString, Encoding.UTF8, "application/json");

            string request = "/api/order/insertitems/" + FakeOrder.OrderID;
            var response = await Client.PostAsync(request, Content);
            var actual = response.StatusCode;
            DeleteFakeOrderForTest(FakeOrder.OrderID);

            Assert.Equal(HttpStatusCode.Created, actual);
        }
        [Fact]
        public async void DeleteOrder_IdDoesExists_ShouldReturnOK()
        {
            var FakeOrder = CreateFakeOrderForTests();
            string request = "/api/order/delete/" + FakeOrder.OrderID;
            var response = await Client.DeleteAsync(request);
            var actual = response.StatusCode;

            Assert.Equal(HttpStatusCode.OK, actual);
        }

        [Fact]
        public async void DeleteOrder_IdDoesNotExists_ShouldReturnNotFound()
        {
            Guid FakeGuid = Guid.NewGuid();
            string request = "/api/order/delete/" + FakeGuid;
            var response = await Client.DeleteAsync(request);
            var actual = response.StatusCode;

            Assert.Equal(HttpStatusCode.NotFound, actual);
        }

        [Fact]
        public async void UpdateOrder_ShouldReturnOK()
        {
            var FakeOrder = CreateFakeOrderForTests();
            var FakeUpdatedOrder = new Orders()
            { OrderID = FakeOrder.OrderID, OrderMadeAt = DateTime.Now, TotalSum = 15, UserID = "FakeUpdatedUserID" };

            var JsonString = JsonConvert.SerializeObject(FakeUpdatedOrder);
            var Content = new StringContent(JsonString, Encoding.UTF8, "application/json");

            string request = "/api/order/update";
            var response = await Client.PutAsync(request, Content);
            var actual = response.StatusCode;
            DeleteFakeOrderForTest(FakeOrder.OrderID);

            Assert.Equal(HttpStatusCode.OK, actual);
        }

        private Orders CreateFakeOrderForTests()
        {
            Orders InsertFakeOrder = new Orders()
            { OrderID = Guid.NewGuid(), OrderMadeAt = DateTime.Now, TotalSum = 15, UserID = "FakeUserID" };
            _context.Orders.Add(InsertFakeOrder);
            _context.SaveChanges();

            return InsertFakeOrder;
        }
        private void DeleteFakeOrderForTest(Guid ID)
        {
            var DeleteProduct = _context.Orders.Where(e => e.OrderID == ID);

            Orders _Orders = new Orders();
            _Orders = DeleteProduct.Single();
            _context.Orders.Remove(_Orders);
            _context.SaveChanges();
        }
    }
}
