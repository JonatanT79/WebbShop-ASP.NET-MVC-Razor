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
            var ProductByID = _context.Products.Where(e => e.ID == ID);
            return ProductByID.Single();
        }
        public void CreateProduct(Products product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void DeleteProduct(int ID)
        {
            var DeleteProduct = _context.Products.Where(e => e.ID == ID);
            var _products = DeleteProduct.Single();
            _context.Products.Remove(_products);
            _context.SaveChanges();
        }
        public void UpdateProduct(Products product)
        {
            var FindProduct = (from e in _context.Products
                               where e.ID == product.ID
                               select e).SingleOrDefault();

            FindProduct.Name = product.Name;
            FindProduct.Description = product.Description;
            FindProduct.Price = product.Price;
            FindProduct.InStock = product.InStock;
            FindProduct.Maker = product.Maker;

            _context.SaveChanges();
        }
    }
}
