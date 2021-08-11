using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CategoryService(userId);
            var model = service.GetCategory();
            return View(model);
        }
        //Get
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryCreate model)
        {
            if (!ModelState.IsValid) return View(model);
            var service = CreateCategoryService();

            if (service.CreateCategory(model))
            {
                TempData["SaveResult"] = "Your category was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Note could not be created.");
            return View(model);
        }
        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CategoryService(userId);
            return service;
        }
        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            var svc = CreateCategoryService();
            var model = svc.GetCategoryById(id);
            return View(model);
        }
        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            var service = CreateCategoryService();
            var detail = service.GetCategoryById(id);
            var model =
                new CategoryEdit
                {
                    CategoryID = detail.CategoryID,
                    Name = detail.Name,
                };
            return View(model);
        }
        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CategoryEdit model)
        {
            try
            {
                if (!ModelState.IsValid) return View(model);
                if (model.CategoryID != id)
                {
                    ModelState.AddModelError("", "id Mismatch");
                    return View(model);
                }

                var service = CreateCategoryService();
                if (service.UpdateCategory(model))
                {
                    TempData["SaveResult"] = "Your Category was Updated.";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Your Category could not be updated");
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Category/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateCategoryService();
            var model = service.GetCategoryById(id);
            return View(model);
        }

        // POST: Note/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            // TODO: Add delete logic here
            var service = CreateCategoryService();
            service.DeleteCategory(id);
            TempData["SaveResult"] = "Your category was deleted";

            return RedirectToAction("Index");
        }
    }
}