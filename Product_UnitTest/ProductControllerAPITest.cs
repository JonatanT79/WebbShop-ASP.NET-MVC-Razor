using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.API;
using Product.API.Controllers;
using Product.API.Data;
using Product.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Product_UnitTest
{
    public class ProductControllerAPITest : IClassFixture<TestFixture<Startup>>
    {
        ProductContext _context = new ProductContext();
        private HttpClient Client;

        public ProductControllerAPITest(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async void GetProducts_ShouldReturnOk()
        {
            using (var _client = new TestFixture<Startup>().Client)
            {
                _client.DefaultRequestHeaders.Add("ReadApiKey", "SecretProductApiKey");
                string request = "api/product";
                var response = await _client.GetAsync(request);
                var actual = response.StatusCode;

                Assert.Equal(HttpStatusCode.OK, actual);
            }
        }

        [Fact]
        public async void GetProducts_ShouldReturnProductList()
        {
            using (var _client = new TestFixture<Startup>().Client)
            {
                _client.DefaultRequestHeaders.Add("ReadApiKey", "SecretProductApiKey");
                string request = "api/product";
                var response = await _client.GetAsync(request);
                var StringContent = await response.Content.ReadAsStringAsync();
                var actual = JsonConvert.DeserializeObject<List<Products>>(StringContent);

                Assert.IsType<List<Products>>(actual);
            }
        }

        [Fact]
        public async void GetProductByID_1_ShouldReturnOk()
        {
            using (var _client = new TestFixture<Startup>().Client)
            {
                _client.DefaultRequestHeaders.Add("ReadApiKey", "SecretProductApiKey");
                string request = "/api/product/" + 1;
                var response = await _client.GetAsync(request);
                var actual = response.StatusCode;

                Assert.Equal(HttpStatusCode.OK, actual);
            }
        }

        [Fact]
        public async void GetProductByID_1_ShouldReturnProduct()
        {
            using (var _client = new TestFixture<Startup>().Client)
            {
                _client.DefaultRequestHeaders.Add("ReadApiKey", "SecretProductApiKey");
                string request = "/api/product/" + 1;
                var response = await _client.GetAsync(request);
                string ResponseString = await response.Content.ReadAsStringAsync();
                var Product = JsonConvert.DeserializeObject<Products>(ResponseString);

                Assert.IsType<Products>(Product);
            }
        }

        [Fact]
        public async void GetProductByID_0_ShouldReturnNotFound()
        {
            using (var _client = new TestFixture<Startup>().Client)
            {
                _client.DefaultRequestHeaders.Add("ReadApiKey", "SecretProductApiKey");
                string request = "/api/product/" + 0;
                var response = await _client.GetAsync(request);
                var actual = response.StatusCode;

                Assert.Equal(HttpStatusCode.NotFound, actual);
            }
        }

        [Fact]
        public async void InsertNewProduct_ShouldReturnCreate()
        {
            using (var _client = new TestFixture<Startup>().Client)
            {
                _client.DefaultRequestHeaders.Add("ReadApiKey", "SecretProductApiKey");
                var FakeProduct = new Products()
                { Name = "FakeInsert", Description = "FakeInsert", Price = 15, InStock = 1, Maker = "FakeMaker" };
                string JsonString = JsonConvert.SerializeObject(FakeProduct);
                var Content = new StringContent(JsonString, Encoding.UTF8, "application/json");

                string request = "api/product/insert";
                var response = await _client.PostAsync(request, Content);

                string ResponseString = await response.Content.ReadAsStringAsync();
                var DeleteProduct = JsonConvert.DeserializeObject<Products>(ResponseString);
                var actual = response.StatusCode;

                DeleteFakeProductForTest(DeleteProduct.ID);

                Assert.Equal(HttpStatusCode.Created, actual);
            }
        }
        [Fact]
        public async void InsertNewProduct_ShouldReturnAProduct()
        {
            using (var _client = new TestFixture<Startup>().Client)
            {
                _client.DefaultRequestHeaders.Add("ReadApiKey", "SecretProductApiKey");
                var FakeProduct = new Products()
                { Name = "FakeInsert", Description = "FakeInsert", Price = 15, InStock = 1, Maker = "FakeMaker" };
                string JsonString = JsonConvert.SerializeObject(FakeProduct);
                var Content = new StringContent(JsonString, Encoding.UTF8, "application/json");

                string request = "api/product/insert";
                var response = await _client.PostAsync(request, Content);

                string ResponseString = await response.Content.ReadAsStringAsync();
                var actual = JsonConvert.DeserializeObject<Products>(ResponseString);

                DeleteFakeProductForTest(actual.ID);

                Assert.IsType<Products>(actual);
            }
        }

        [Fact]
        public async void DeleteProduct_IdDoesExists_ShouldReturnOK()
        {
            using (var _client = new TestFixture<Startup>().Client)
            {
                _client.DefaultRequestHeaders.Add("ReadApiKey", "SecretProductApiKey");
                var FakeProduct = CreateFakeProductForTests();
                string request = "/api/product/delete/" + FakeProduct.ID;
                var response = await _client.DeleteAsync(request);
                var actual = response.StatusCode;

                Assert.Equal(HttpStatusCode.OK, actual);
            }
        }

        [Fact]
        public async void DeleteProduct_IdDoesNotExists_ShouldReturnNotFound()
        {
            using (var _client = new TestFixture<Startup>().Client)
            {
                _client.DefaultRequestHeaders.Add("ReadApiKey", "SecretProductApiKey");
                string request = "/api/product/delete/" + 0;
                var response = await _client.DeleteAsync(request);
                var actual = response.StatusCode;

                Assert.Equal(HttpStatusCode.NotFound, actual);
            }
        }

        [Fact]
        public async void UpdateProduct_ShouldReturnOK()
        {
            using (var _client = new TestFixture<Startup>().Client)
            {
                _client.DefaultRequestHeaders.Add("ReadApiKey", "SecretProductApiKey");
                var FakeProduct = CreateFakeProductForTests();
                var FakeUpdatedProduct = new Products()
                { ID = FakeProduct.ID, Name = "FakeUpdateInsert", Description = "FakeUpdateInsert", Price = 15, InStock = 1, Maker = "FakeUpdateMaker" };

                var JsonString = JsonConvert.SerializeObject(FakeUpdatedProduct);
                var Content = new StringContent(JsonString, Encoding.UTF8, "application/json");

                string request = "/api/product/update";
                var response = await _client.PutAsync(request, Content);
                var actual = response.StatusCode;
                DeleteFakeProductForTest(FakeProduct.ID);

                Assert.Equal(HttpStatusCode.OK, actual);
            }
        }

        private Products CreateFakeProductForTests()
        {
            Products InsertFakeProduct = new Products()
            { Name = "FakeInsert", Description = "FakeInsert", Price = 15, InStock = 1, Maker = "FakeMaker" };
            _context.Products.Add(InsertFakeProduct);
            _context.SaveChanges();

            return InsertFakeProduct;
        }
        private void DeleteFakeProductForTest(int ID)
        {
            var DeleteProduct = _context.Products.Where(e => e.ID == ID);
            var _products = DeleteProduct.Single();

            _context.Products.Remove(_products);
            _context.SaveChanges();
        }
    }
}
