using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            IEnumerable<ProductCategoryViewModel> data = (from p in Productdb.Product
                          join c in Productdb.Category on p.ProductCategoryId equals c.CategoryId
                          select new ProductCategoryViewModel
                          {
                              ProductId = p.ProductId,
                              ProductName = p.ProductName,
                              CategoryId = c.CategoryId,
                              CategoryName = c.CategoryName
                          }).ToList();



            //IEnumerable<Product> data= Productdb.Product.OrderBy(p=>p.ProductId).Include(p=>p.Category).ToList<Product>();
            ViewBag.TotalCount = data.Count();
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = page;
             data = data.Skip((page-1)*10).Take(10).ToList();

            return View(data);
        }
        

        public ActionResult Create()
        {
            var CategoryList = Productdb.Category.ToList();
            ViewBag.Catlist = new SelectList(CategoryList, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {

            Productdb.Product.Add(product);
            Productdb.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return RedirectToAction("Index");
            }
            var product =Productdb.Product.Single(x=>x.ProductId==id);
            
            var categories = Productdb.Category
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName,
                        Selected = (c.CategoryId == product.ProductCategoryId)  // Pre-select the current category
                    }).ToList();

            // Create a ViewModel for the edit form
            var model = new ProductEditViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.ProductCategoryId,  // Current category
                Categories = categories                  // Categories for the dropdown
            };

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(int id,ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the product to update
                var product = Productdb.Product.FirstOrDefault(p => p.ProductId == model.ProductId);

                if (product == null)
                {
                    return HttpNotFound();
                }

                // Update product details
                product.ProductName = model.ProductName;
                product.ProductCategoryId = model.CategoryId;  // Update with the new selected category

                // Save changes to the database
                Productdb.SaveChanges();

                return RedirectToAction("Index");
            }
            // If model is invalid, return the view with categories populated again
            var categories = Productdb.Category
                            .Select(c => new SelectListItem
                            {
                                Value = c.CategoryId.ToString(),
                                Text = c.CategoryName
                            }).ToList();

            
            model.Categories = categories;  // Re-populate categories for the dropdown in case of validation errors
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if(id==null)
            {
                return RedirectToAction("Index");
            }
            var data=Productdb.Product.Single(x=>x.ProductId==id);
            return View(data);
        }

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
        public ActionResult Delete(int id,Product product)
        {
            Product data = Productdb.Product.Single(x => x.ProductId == id);
            Productdb.Product.Remove(data);
            Productdb.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}