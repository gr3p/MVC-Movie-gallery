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

            var repo = new MovieGalleryRepository();
            var detailsAboutMovie = repo.GetDetailsAboutMovie(id);
            detailsAboutMovie.Trailers = repo.GetMovieTrailers(id);
            detailsAboutMovie.Credits = repo.GetMovieCredits(id);
            return View("~/views/moviegallery/detail.cshtml",detailsAboutMovie);
        }
        
        public ActionResult Index()
        {
            ViewBag.viewheadline = "DVD/Blu-ray News";
            var repo = new MovieGalleryRepository();
            var movieRepo = repo.GetRecentReleasedMovies();

            var viewModel = new SearchMovieViewModel<MovieSearchItems>(repo, movieRepo.MovieItems, movieRepo.page, movieRepo.total_pages, movieRepo.total_results);
            return View("~/Views/MovieGallery/Index.cshtml",viewModel);
        }
        public ActionResult ComingSoon()
        {
            ViewBag.viewheadline = "Coming Soon on DVD/Blu-ray";
            var repo = new MovieGalleryRepository();
            var movieRepo = repo.MoviesComingSoon();
            var viewModel = new SearchMovieViewModel<MovieSearchItems>(repo, movieRepo.MovieItems, movieRepo.page, movieRepo.total_pages, movieRepo.total_results);
            return View("~/Views/MovieGallery/Index.cshtml",viewModel);
        }



        public ActionResult LookUpMovie()
        {
            return View("~/Views/MovieGallery/LookUpMovie.cshtml");
        }


        public ActionResult LookUpLocalMovies()
        {
            using (var context = new Context.Context())
            {
                var comicBooks = context.movieSearchItems.ToList();

                return View("~/Views/MovieGallery/LookUpLocalMovies.cshtml", comicBooks);
            }
        }

        [HttpPost]
        public ActionResult LookUpMovie(string movieToFind)
        {
            if (string.IsNullOrEmpty(movieToFind))
            {
                return Redirect("Index");
            }
            ViewBag.SearchingFor = movieToFind;
            var movieRepository = new MovieGalleryRepository();
            var movieItem = movieRepository.SearchForAMovie(movieToFind);
            var viewModel = new SearchMovieViewModel<MovieSearchItems>(movieRepository, movieItem.MovieItems, movieItem.page, movieItem.total_pages, movieItem.total_results);
           
            return View("~/Views/MovieGallery/LookUpMovie.cshtml", viewModel);
        }
    }
}