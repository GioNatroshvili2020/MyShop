using MyShop.core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.core.Contracts;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryController : Controller
    {
        IRepository<ProductCategory> context;

        public ProductCategoryController(IRepository<ProductCategory>  IContext)
        {
            context = IContext;

        }


        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.Commit();
                return RedirectToAction("Index");

            }
        }

        public ActionResult Edit(String Id)
        {
            ProductCategory productCategory = context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, String Id)
        {
            ProductCategory categoryToEdit = context.Find(Id);
            if (categoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }
                else
                {
                    categoryToEdit.Category = productCategory.Category;                    
                    
                    context.Commit();
                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(String Id)
        {
            ProductCategory categoryToDelete = context.Find(Id);
            if (categoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(categoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(String Id)
        {
            ProductCategory categoryToDelete = context.Find(Id);
            if (categoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(categoryToDelete.Id);
                context.Commit();
                return RedirectToAction("Index");
            }


        }
    }
}