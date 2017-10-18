using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookGallery.Data;

namespace BookGallery.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookGalleryRepository _bookGalleryRepository;

        public HomeController()
        {
            _bookGalleryRepository = new BookGalleryRepository();
        }


        public ActionResult Index()
        {

            return View(_bookGalleryRepository.GetBookGalleryItems());
        }
    }
}