using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using MovieGallery.Models;
using MovieGallery.Models.API;

namespace MovieGallery.Data
{
    public class MovieGalleryRepository
    {
        public MovieGenres MovieGenre { get; set; }

        public MovieGalleryRepository()
        {

            MovieGenre = API.MovieHttpClient.GetMovieGenres();
        }

        public MovieSearchItems SearchForAMovie(string SearchText)
        {
            return API.MovieHttpClient.GetMovieResults(SearchText);

        }
        public MovieDetailsItem GetDetailsAboutMovie(int movieId)
        {
            return API.MovieHttpClient.GetDetailedMovieResults(movieId);

        }

        public MovieSearchItems GetMostPopularMovies()
        {
            return API.MovieHttpClient.GetPopularMovies();
        }
    }
}