using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMVirtualGallery.WebMVC.Controllers
{
    [Authorize]
    public class ExhibitionController : Controller
    {
        // GET: Exhibition
        public ActionResult Index()
        {
            var model = new ExhibitionItems[0];
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
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}