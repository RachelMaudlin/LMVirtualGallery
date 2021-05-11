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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CompositionService(userId);
            service.CreateComposition(model);

            return RedirectToAction("Index");
        }
    }
}