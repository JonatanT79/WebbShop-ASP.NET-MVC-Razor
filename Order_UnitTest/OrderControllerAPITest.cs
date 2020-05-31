using Microsoft.AspNetCore.Mvc;
using Order.API.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Order_UnitTest
{
    public class OrderControllerAPITest
    {
        HttpClient _client = new HttpClient();
        public Uri BaseAdress { get; set; } = new Uri("http://localhost:5000/");

        [Fact]
        public async void GetOrders_ShouldReturnOk()
        {
            var request = BaseAdress + "order";
            var response = await _client.GetAsync(request);
            var actual = response.StatusCode;
            Assert.Equal(HttpStatusCode.OK, actual);
            //error
            //using (var client = new TestClientProvider().Client)
            //{
            //    var response =  await client.GetAsync("api/order");
            //    var actual = response.StatusCode;
            //    Assert.Equal(HttpStatusCode.OK, actual);
            //}
            //var GetActionType = _controller.GetOrders();
            //Assert.IsType<OkObjectResult>(GetActionType);
        }
    }
}
