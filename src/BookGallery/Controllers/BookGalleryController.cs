using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}