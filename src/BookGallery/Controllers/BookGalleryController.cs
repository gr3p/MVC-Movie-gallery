using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookGallery.Data;
using BookGallery.Models;

namespace BookGallery.Controllers
{
    public class BookGalleryController : Controller
    {
        private readonly BookGalleryRepository _bookGalleryRepository;

        public BookGalleryController()
        {
            _bookGalleryRepository = new BookGalleryRepository();
        }

        public ActionResult Detail(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
           var comicBooks = _bookGalleryRepository.GetBookGalleryItem(id.Value);

            return View(comicBooks);
        }
        // GET: BookGallery
        public ActionResult Index()
        {
            var bookGalleries = _bookGalleryRepository.GetBookGalleryItems();
            return View(bookGalleries);
        }

        public ActionResult LookUpMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LookUpMovie(string movieToFind)
        {
            ViewBag.SearchingFor = movieToFind;
            var movieItem = new MovieGalleryRepository().SearchForAMovie(movieToFind);
           
            return View(movieItem);
        }
    }
}