using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookGallery.Models;

namespace BookGallery.Controllers
{
    public class BookGalleryController : Controller
    {


        public ActionResult Detail()
        {
           var comicBooks = new Data.BookGalleryRepository().GetBookGalleryItems();

            return View(comicBooks);
        }
        // GET: BookGallery
        public ActionResult Index()
        {
            return View();
        }
    }
}