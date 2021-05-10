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
    }
}