using LMVirtualGallery.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMVirtualGallery.WebMVC.Controllers
{
    public class CompositionController : Controller
    {
        // GET: Composition
        [Authorize]
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CompositionService(userId);
            var model = service.GetCompositions();

            return View(model);
        }

        //GET/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompositionCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCompositionService();
            if (service.CreateComposition(model))
            {
                TempData["SaveResult"] = "Your composition was added.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Composition could not be added.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateCompositionService();
            var model= svc.GetCompositionById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateCompositionService();
            var detail = service.GetCompositionById(id);
            var model =
                new CompositionEdit
                {
                    CompositionId = detail.CompositionId,
                    CompositionName = detail.CompositionName,
                    CompositionMedium = detail.CompositionMedium,
                    ImageName = detail.ImageName,
                    CompositionDescription = detail.CompositionDescription,
                    CompositionCreation = detail.CompositionCreation
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CompositionEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.CompositionId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCompositionService();
            if (service.UpdateComposition(model))
            {
                TempData["SaveResult"] = "Your composition was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your composition could not be updated.");
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var svc = CreateCompositionService();
            var model = svc.GetCompositionById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateCompositionService();
            service.DeleteComposition(id);
            TempData["SaveResult"] = "Your composition was deleted.";
            return RedirectToAction("Index");
        }

        private CompositionService CreateCompositionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CompositionService(userId);
            return service;
        }
    }
}