using LMVirtualGallery.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;

namespace LMVirtualGallery.WebMVC.Controllers
{
    [Authorize]
    public class ExhibitionController : Controller
    {
        // GET: Exhibition
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ExhibitionService(userId);
            var model = service.GetExhibitions();
            return View(model);
        }

        //GET/Create Exhibition
        public ActionResult Create()
        {
            return View();
        }

        //POST/Create Exhibition
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExhibitionCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateExhibitionService();

            if(service.CreateExhibition(model))
            {
                TempData["SaveResult"] = "Your exhibition was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Exhibition could not be created.");
            return View(model);
        }

        private ExhibitionService CreateExhibitionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ExhibitionService(userId);
            return service;
        }

        public ActionResult Details(int id)
        {
            var svc = CreateExhibitionService();
            var model = svc.GetExhibitionById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateExhibitionService();
            var detail = service.GetExhibitionById(id);
            var model =
                new ExhibitionEdit
                {
                    ExhibitionId = detail.ExhibitionId,
                    ExhibitionName = detail.ExhibitionName,
                    ExhibitionDescription = detail.ExhibitionDescription,
                    ExhibitionDate = detail.ExhibitionDate,
                    ExhibitionLocation = detail.ExhibitionLocation
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,ExhibitionEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.ExhibitionId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateExhibitionService();

            if (service.UpdateExhibition(model))
            {
                TempData["SaveResult"] = "Your exhibition was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your exhibition could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateExhibitionService();
            var model = svc.GetExhibitionById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateExhibitionService();

            service.DeleteExhibition(id);

            TempData["SaveResult"] = "Your exhibition was deleted.";
            return RedirectToAction("Index");
        }
    }
}