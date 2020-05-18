using Product.API.Data;
using Product.API.Models;
using Product.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Product_UnitTest
{
    public class RepositoryTest
    {
        ProductRepository _productRepository = new ProductRepository();
        readonly ProductContext _context = new ProductContext();

        [Fact]
        public void ShouldGetAllProducts()
        {
            var expected = from e in _context.Products
                           select e.ID + " " + e.Name;

            var actual = from e in _productRepository.GetAllProducts()
                         select e.ID + " " + e.Name;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldGetProductByID()
        {
            int TestID = 2;
            var actual = _productRepository.GetProductByID(TestID);
            Assert.IsType<Products>(actual);
        }

        [Fact]
        public void ShouldInsertProduct()
        {
            int CountBeforeInsert = _context.Products.Count();
            Products FakeProduct = new Products() { Name = "Fake", Description = "Fake", Price = 15, InStock = 1, Maker = "FakeMaker"};
            _productRepository.CreateProduct(FakeProduct);
            int CountAfterInsert = _context.Products.Count();

            //Delete the Fakeproduct that was inserted
            var FindFakeProduct = _context.Products.Where(e => e.Name == "Fake");
            var DeleteProduct = FindFakeProduct.Single();
            _context.Products.Remove(DeleteProduct);
            _context.SaveChanges();

            Assert.Equal((CountBeforeInsert + 1), CountAfterInsert);
        }
        [Fact]
        public void ShouldDeleteProduct()
        {
            Products InsertFakeProduct = new Products()
            { Name = "FakeInsert", Description = "FakeInsert", Price = 15, InStock = 1, Maker = "FakeMaker"};
            _context.Products.Add(InsertFakeProduct);
            _context.SaveChanges();
            int CountBeforeDelete = _context.Products.Count();

            _productRepository.DeleteProduct(InsertFakeProduct.ID);
            int CountAfterDelete = _context.Products.Count();

            Assert.Equal((CountBeforeDelete - 1), CountAfterDelete);
        }
        [Fact]
        public void ShouldUpdateProduct()
        {
            Products InsertFakeProduct = new Products()
            { Name = "FakeProduct", Description = "FakeProduct", Price = 15, InStock = 1, Maker = "FakeMaker"};
            _productRepository.CreateProduct(InsertFakeProduct);

            Products UpdateFakeProduct = new Products()
            { 
                ID = InsertFakeProduct.ID, Name = "UpdateFakeProduct", Description = "UpdateFakeProduct",
                Price = 55, InStock = 5, Maker = "FakeMaker"
            };
            
            _productRepository.UpdateProduct(UpdateFakeProduct);
            var GetProductInDB = _context.Products.Where(e => e.ID == InsertFakeProduct.ID);
            var ProductShouldBeUpdated = GetProductInDB.First();
            //samma frutom null
            Assert.NotEqual(InsertFakeProduct, ProductShouldBeUpdated);

            //Delete FakeProduct
            _productRepository.DeleteProduct(InsertFakeProduct.ID);
        }
    }
}
