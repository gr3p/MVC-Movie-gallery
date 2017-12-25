using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieGallery.Data;
using MovieGallery.Models;
using MovieGallery.Models.ViewModels;

namespace MovieGallery.Controllers
{
    public class MovieGalleryController : Controller
    {
        

        public ActionResult Detail(int id)
        {
            if(id == 0)
            {
                return HttpNotFound();
            }
           
            var detailsAboutMovie = new MovieGalleryRepository().GetDetailsAboutMovie(id);

            return View(detailsAboutMovie);
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


        //public ActionResult LookUpLocalMovies()
        //{
        //    var getLocalMoviesFromFolder = 
        //    return View();
        //}

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