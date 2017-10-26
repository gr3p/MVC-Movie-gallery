using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookGallery.Data;
using BookGallery.Models;
using BookGallery.Models.ViewModels;

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
        
        public ActionResult Index()
        {
            var repo = new MovieGalleryRepository();
            var movieRepo = repo.GetMostPopularMovies();
            var viewModel = new SearchMovieViewModel<MovieSearchItems>(repo, movieRepo.MovieItems, movieRepo.page, movieRepo.total_pages, movieRepo.total_results);
            return View(viewModel);
        }

        public ActionResult LookUpMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LookUpMovie(string movieToFind)
        {
            ViewBag.SearchingFor = movieToFind;
            var movieRepository = new MovieGalleryRepository();
            var movieItem = movieRepository.SearchForAMovie(movieToFind);
            var viewModel = new SearchMovieViewModel<MovieSearchItems>(movieRepository, movieItem.MovieItems, movieItem.page, movieItem.total_pages, movieItem.total_results);
            return View(viewModel);
        }
    }
}