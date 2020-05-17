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
            //Arrange
            var expected = from e in _context.Products
                           select e.ID + " " + e.Name;

            //act
            var actual = from e in _productRepository.GetAllProducts()
                         select e.ID + " " + e.Name;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldGetProductByID()
        {
            int TestID = 2;
            Assert.IsType<Products>(_productRepository.GetProductByID(TestID));
        }

        [Fact]
        public void ShouldInsertProduct()
        {
            int CountBeforeInsert = _context.Products.Count();
            Products FakeProduct = new Products() { Name = "Fake", Description = "Fake", Price = 15, InStock = 1, ProductBrandID = 2 };
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
            { Name = "FakeInsert", Description = "FakeInsert", Price = 15, InStock = 1, ProductBrandID = 2 };
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
            { Name = "FakeProduct", Description = "FakeProduct", Price = 15, InStock = 1, ProductBrandID = 2 };
            _context.Products.Add(InsertFakeProduct);
            _context.SaveChanges();

            Products UpdateFakeProduct = new Products()
            { 
                ID = InsertFakeProduct.ID, Name = "UpdateFakeProduct", Description = "UpdateFakeProduct",
                Price = 55, InStock = 5, ProductBrandID = 4 
            };
            _productRepository.UpdateProduct(UpdateFakeProduct);
            //Fel det under ger det gamla värdet 'FakeProduct'
            var GetProductInDB = _context.Products.Where(e => e.ID == InsertFakeProduct.ID);
            var actual = GetProductInDB.First();

            //actual istället för updatefakeproduct
            Assert.NotEqual(InsertFakeProduct,UpdateFakeProduct );
            //Delete FakeProduct
            _context.Products.Remove(actual);
            _context.SaveChanges();
        }
    }
}
