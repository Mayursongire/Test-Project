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
        ApplicationDbContext CategoryDb = new ApplicationDbContext();

        // GET: Category
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var data = CategoryDb.Category.Where(c => c.IsActive == true).ToList(); // Only active categories

            ViewBag.TotalCount = data.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page;

            // Apply pagination
            data = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(data);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.IsActive = true;  // Default to active
                CategoryDb.Category.Add(category);
                CategoryDb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Category/Edit
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
                var category = CategoryDb.Category.FirstOrDefault(p => p.CategoryId == model.CategoryId);

                if (category == null)
                {
                    return HttpNotFound();
                }

                category.CategoryName = model.CategoryName;
                category.IsActive = model.IsActive;  // Update IsActive field

                CategoryDb.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Category/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var data = CategoryDb.Category.Single(x => x.CategoryId == id);
            return View(data);
        }

        // GET: Category/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var category = CategoryDb.Category.Single(x => x.CategoryId == id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Delete(int id, Category category)
        {
            var data = CategoryDb.Category.Single(x => x.CategoryId == id);
            CategoryDb.Category.Remove(data);
            CategoryDb.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
