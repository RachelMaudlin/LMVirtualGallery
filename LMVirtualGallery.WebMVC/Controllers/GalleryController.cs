using LMVirtualGallery.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMVirtualGallery.WebMVC.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GalleryService(userId);
            var model = service.GetGalleries();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GalleryCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateGalleryService();

            if (service.CreateGallery(model))
            {
                TempData["SaveResult"] = "Your gallery was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Gallery could not be created.");
            return View(model);
        }

        private GalleryService CreateGalleryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GalleryService(userId);
            return service;
        }

        public ActionResult Details(int id)
        {
            var svc = CreateGalleryService();
            var model = svc.GetGalleryById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateGalleryService();
            var detail = service.GetGalleryById(id);
            var model =
                new ExhibitionEdit
                {

                };
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateGalleryService();
            var model = svc.GetGalleryById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateGalleryService();

            service.DeleteGallery(id);

            TempData["SaveResult"] = "Your gallery was deleted.";
            return RedirectToAction("Index");
        }

 

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public ActionResult Edit(int id, GalleryEdit model)
    //{
    // if (!ModelState.IsValid) return View(model);

    // if (model.ExhibitionId != id)
    //{
    //ModelState.AddModelError("", "Id Mismatch");
    //return View(model);
    //}
    //
    //var service = CreateExhibitionService();

    //if (service.UpdateExhibition(model))
    // {
    // TempData["SaveResult"] = "Your exhibition was updated.";
    // return RedirectToAction("Index");
    //}

    //ModelState.AddModelError("", "Your exhibition could not be updated.");
    //return View(model);
    //}

 
    }
}
