using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMVirtualGallery.WebMVC.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            var model = new EventItems[0];
            return View();
        }

        //GET:Create/Event
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventCreate model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }

    
  
}