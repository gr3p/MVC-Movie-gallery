using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookGallery.Controllers
{
    public class BookGalleryController : Controller
    {


        public ActionResult Detail()
        {
            return View();
        }
        // GET: BookGallery
        public ActionResult Index()
        {
            return View();
        }
    }
}