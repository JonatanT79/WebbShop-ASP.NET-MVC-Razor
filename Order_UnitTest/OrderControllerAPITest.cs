using Microsoft.AspNetCore.Mvc;
using Order.API.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Order_UnitTest
{
    public class OrderControllerAPITest
    {
        OrderController _controller = new OrderController();
        [Fact]
        public void GetOrders_ShouldReturnOk()
        {
            var GetActionType = _controller.GetOrders();
            Assert.IsType<OkObjectResult>(GetActionType);
        }
    }
}
