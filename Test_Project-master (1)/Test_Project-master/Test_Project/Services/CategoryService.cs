using System;
using System.Collections.Generic;
using System.Linq;
using Test_Project.EF;
using Test_Project.Models;

namespace Test_Project.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject ApplicationDbContext
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add a new category
        public void AddCategory(Category category)
        {
            // Check if category already exists
            if (_context.Category.Any(c => c.CategoryName == category.CategoryName))
            {
                throw new Exception("Category already exists.");
            }

            // Add the category to database
            _context.Category.Add(category);
            _context.SaveChanges();
        }

        // Update an existing category
        public void UpdateCategory(Category category)
        {
            var existingCategory = _context.Category.SingleOrDefault(c => c.CategoryId == category.CategoryId);
            if (existingCategory == null)
            {
                throw new Exception("Category not found.");
            }

            existingCategory.CategoryName = category.CategoryName;
            _context.SaveChanges();
        }

        // Get all categories
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Category.ToList();
        }

        // Get a category by ID
        public Category GetCategoryById(int id)
        {
            return _context.Category.SingleOrDefault(c => c.CategoryId == id);
        }

        // Delete a category
        public void DeleteCategory(int id)
        {
            var category = _context.Category.SingleOrDefault(c => c.CategoryId == id);
            if (category != null)
            {
                _context.Category.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}
