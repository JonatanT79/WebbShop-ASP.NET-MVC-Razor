using Product.API.Data;
using Product.API.Models;
using Product.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Product_UnitTest
{
    public class ProductRepositoryTest
    {
        ProductRepository _productRepository = new ProductRepository();
        readonly ProductContext _context = new ProductContext();

        [Fact]
        public void GetAllProducts_ShouldGetAllProductsWithID()
        {
            var expected = from e in _context.Products
                           select e.ID;

            var actual = from e in _productRepository.GetAllProducts()
                         select e.ID;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetProductByID_ShouldGetProductByID()
        {
            int TestID = 2;
            var actual = _productRepository.GetProductByID(TestID);
            Assert.IsType<Products>(actual);
        }

        [Fact]
        public void CreateProduct_ShouldInsertProduct()
        {
            int CountBeforeInsert = _context.Products.Count();
            Products FakeProduct = new Products() { Name = "Fake", Description = "Fake", Price = 15, InStock = 1, Maker = "FakeMaker" };
            _productRepository.CreateProduct(FakeProduct);
            int CountAfterInsert = _context.Products.Count();
            DeleteFakeProductForTest(FakeProduct.ID);

            Assert.Equal((CountBeforeInsert + 1), CountAfterInsert);
        }
        [Fact]
        public void DeleteProduct_ShouldDeleteProduct()
        {
            var InsertFakeProduct = CreateFakeProductForTests();
            int CountBeforeDelete = _context.Products.Count();
            _productRepository.DeleteProduct(InsertFakeProduct.ID);
            int CountAfterDelete = _context.Products.Count();

            Assert.Equal((CountBeforeDelete - 1), CountAfterDelete);
        }
        [Fact]
        public void UpdateProduct_ShouldUpdateProduct()
        {
            Products InsertFakeProduct = new Products()
            { Name = "FakeProduct", Description = "FakeProduct", Price = 15, InStock = 1, Maker = "FakeMaker"};
            _productRepository.CreateProduct(InsertFakeProduct);

            Products UpdateFakeProduct = new Products()
            { 
                ID = InsertFakeProduct.ID, Name = "UpdateFakeProduct", Description = "UpdateFakeProduct",
                Price = 55, InStock = 5, Maker = "UpdateFakeMaker"
            };
            
            _productRepository.UpdateProduct(UpdateFakeProduct);
            var GetProductInDB = _context.Products.Where(e => e.ID == InsertFakeProduct.ID);
            var ProductShouldBeUpdated = GetProductInDB.First();
            Assert.NotEqual(InsertFakeProduct, ProductShouldBeUpdated);
            DeleteFakeProductForTest(InsertFakeProduct.ID);
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

            Products _products = new Products();
            _products = DeleteProduct.Single();

            _context.Products.Remove(_products);
            _context.SaveChanges();
        }
    }
}
