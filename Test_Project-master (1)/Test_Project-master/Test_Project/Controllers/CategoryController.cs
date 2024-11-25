using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_Project.EF;
using Test_Project.Models;

namespace Test_Project.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext CategoryDb = new ApplicationDbContext()
;        // GET: Category
        public ActionResult Index()
        {
            var data = CategoryDb.Category.ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoryDb.Category.Add(category);
                CategoryDb.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var category = CategoryDb.Category.Single(x => x.CategoryId == id);

            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(int id, Category model)
        {
            if (ModelState.IsValid)
            {
                // Find the product to update
                var category = CategoryDb.Category.FirstOrDefault(p => p.CategoryId == model.CategoryId );

                if (category == null)
                {
                    return HttpNotFound();
                }

                // Update product details
                category.CategoryName = model.CategoryName;
                  // Update with the new selected category

                // Save changes to the database
                CategoryDb.SaveChanges();

                return RedirectToAction("Index");
            }
            // If model is invalid, return the view with categories populated again
            return View("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var data = CategoryDb.Category.Single(x => x.CategoryId == id);
            return View(data);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var product = CategoryDb.Category.Single(x => x.CategoryId == id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(int id, Category category)
        {
            Category data = CategoryDb.Category.Single(x => x.CategoryId == id);
            CategoryDb.Category.Remove(data);
            CategoryDb.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}