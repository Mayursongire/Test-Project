using System;
using System.Collections.Generic;
using System.Linq;
using Test_Project.EF;
using Test_Project.Models;

namespace Test_Project.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject ApplicationDbContext
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add new product to the database
        public void AddProduct(Product product)
        {
            // Business logic: Check if the product name is already taken
            if (_context.Product.Any(p => p.ProductName == product.ProductName))
            {
                throw new Exception("Product name already exists.");
            }

            // Add product
            _context.Product.Add(product);
            _context.SaveChanges();
        }

        // Update existing product
        public void UpdateProduct(Product product)
        {
            var existingProduct = _context.Product.SingleOrDefault(p => p.ProductId == product.ProductId);
            if (existingProduct == null)
            {
                throw new Exception("Product not found.");
            }

            existingProduct.ProductName = product.ProductName;
            existingProduct.ProductCategoryId = product.ProductCategoryId;
            _context.SaveChanges();
        }

        // Get all products
        public IEnumerable<ProductCategoryViewModel> GetAllProducts()
        {
            var data = (from p in _context.Product
                        join c in _context.Category on p.ProductCategoryId equals c.CategoryId
                        select new ProductCategoryViewModel
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            CategoryId = c.CategoryId,
                            CategoryName = c.CategoryName
                        }).ToList();

            return data;
        }

        // Get a single product by ID
        public Product GetProductById(int id)
        {
            return _context.Product.SingleOrDefault(p => p.ProductId == id);
        }

        // Delete a product
        public void DeleteProduct(int id)
        {
            var product = _context.Product.SingleOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                _context.Product.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}
