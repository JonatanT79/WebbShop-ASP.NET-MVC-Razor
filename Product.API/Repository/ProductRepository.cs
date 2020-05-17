using Product.API.Data;
using Product.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.Repository
{
    public class ProductRepository : IProductRepository
    {
        readonly ProductContext _context = new ProductContext();
        public List<Products> GetAllProducts()
        {
            var FullProductsList = _context.Products.ToList();

            return FullProductsList;
        }
        public Products GetProductByID(int ID)
        {
            var ProductByID = from e in _context.Products
                              where e.ID == ID
                              select e;

            return ProductByID.Single();
        }
        public void CreateProduct(Products product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void DeleteProduct(int ID)
        {
            var DeleteProduct = from e in _context.Products
                                where e.ID == ID
                                select e;

            Products _products = new Products();
            _products = DeleteProduct.Single();

            _context.Products.Remove(_products);
            _context.SaveChanges();
        }
        public void UpdateProduct(Products product) 
        {
            var FindProduct = from e in _context.Products
                              where e.ID == product.ID
                              select e;

            foreach (var item in FindProduct)
            {
                item.Name = product.Name;
                item.Description = product.Description;
                item.Price = product.Price;
                item.InStock = product.InStock;
                item.ProductBrandID = product.ProductBrandID;
            }

            _context.SaveChanges();
        }
    }
}
