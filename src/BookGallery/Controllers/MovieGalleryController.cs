using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
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
            var bluerayreleasedate = repo.GetBlueRayReleaseDate(id);
            detailsAboutMovie.bluerayrelease = bluerayreleasedate != null ? bluerayreleasedate.release_date.ToString("yyyy-MM-dd") : "N/A";
            

            return View("~/views/moviegallery/detail.cshtml", detailsAboutMovie);
        }
        
        public ActionResult Index()
        {
            ViewBag.viewheadline = "DVD/Blu-ray News";
            ViewBag.currentNews = "active-menu";
            var repo = new MovieGalleryRepository();
            var movieRepo = repo.GetRecentReleasedMovies();
            var viewModel = new SearchMovieViewModel<MovieSearchItems>(repo, movieRepo.MovieItems, movieRepo.page, movieRepo.total_pages, movieRepo.total_results);

            return View("~/Views/MovieGallery/Index.cshtml", viewModel);
        }
        public ActionResult ComingSoon()
        {
            ViewBag.viewheadline = "Coming Soon on DVD/Blu-ray";
            ViewBag.comingSoon = "active-menu";
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
        public ActionResult LookUpMovie(string movieToFind, FormCollection form)
        {
            if (string.IsNullOrEmpty(movieToFind))
            {
                return Redirect("Index");
            }

            if (form["SearchType"] != null && form["SearchType"] == SearchTypeSelect.DropdownSelect.Actors.ToString())
            {
               return new MovieGalleryController().LookUpActor(movieToFind);
            };
            ViewBag.SearchingFor = movieToFind;
            var movieRepository = new MovieGalleryRepository();
            var movieItem = movieRepository.SearchForAMovie(movieToFind);
            var viewModel = new SearchMovieViewModel<MovieSearchItems>(movieRepository, movieItem.MovieItems, movieItem.page, movieItem.total_pages, movieItem.total_results);
           
            return View("~/Views/MovieGallery/LookUpMovie.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult LookUpActor(string actorToFind)
        {
            if (string.IsNullOrEmpty(actorToFind)) { return Redirect("Index"); }
            ViewBag.SearchingFor = actorToFind;
            var movieRepository = new MovieGalleryRepository();
            var actorItem = movieRepository.SearchForActor(actorToFind);
            var viewModel = new SearchActorViewModel<ActorResultItem>(actorItem);

            return View("~/Views/MovieGallery/LookUpActor.cshtml", viewModel);


        }


    }

    
}