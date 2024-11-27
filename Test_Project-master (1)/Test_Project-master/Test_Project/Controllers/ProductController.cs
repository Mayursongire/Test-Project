using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_Project.EF;
using Test_Project.Models;

namespace Test_Project.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext Productdb = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var data = (from p in Productdb.Product
                        join c in Productdb.Category on p.ProductCategoryId equals c.CategoryId
                        select new ProductCategoryViewModel
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            CategoryId = c.CategoryId,
                            CategoryName = c.CategoryName
                        }).ToList();

            ViewBag.TotalCount = data.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page;

            // Apply pagination
            data = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(data);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            var CategoryList = Productdb.Category.ToList();
            ViewBag.Catlist = new SelectList(CategoryList, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                Productdb.Product.Add(product);
                Productdb.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Product/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var product = Productdb.Product.Single(x => x.ProductId == id);

            var categories = Productdb.Category
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName,
                    Selected = (c.CategoryId == product.ProductCategoryId)
                }).ToList();

            var model = new ProductEditViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.ProductCategoryId,
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = Productdb.Product.FirstOrDefault(p => p.ProductId == model.ProductId);

                if (product == null)
                {
                    return HttpNotFound();
                }

                product.ProductName = model.ProductName;
                product.ProductCategoryId = model.CategoryId;

                Productdb.SaveChanges();
                return RedirectToAction("Index");
            }

            var categories = Productdb.Category
                            .Select(c => new SelectListItem
                            {
                                Value = c.CategoryId.ToString(),
                                Text = c.CategoryName
                            }).ToList();

            model.Categories = categories;
            return View(model);
        }

        // GET: Product/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var data = Productdb.Product.Single(x => x.ProductId == id);
            return View(data);
        }

        // GET: Product/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var product = (from p in Productdb.Product
                           join c in Productdb.Category on p.ProductCategoryId equals c.CategoryId
                           where p.ProductId == id
                           select new ProductCategoryViewModel
                           {
                               ProductId = p.ProductId,
                               ProductName = p.ProductName,
                               CategoryId = c.CategoryId,
                               CategoryName = c.CategoryName
                           }).FirstOrDefault();

            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(int id, Product product)
        {
            Product data = Productdb.Product.Single(x => x.ProductId == id);
            Productdb.Product.Remove(data);
            Productdb.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
