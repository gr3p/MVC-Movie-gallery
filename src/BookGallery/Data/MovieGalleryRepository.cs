using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using BookGallery.Models;
using BookGallery.Models.API;

namespace BookGallery.Data
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

        public MovieSearchItems GetMostPopularMovies()
        {
            return API.MovieHttpClient.GetPopularMovies();
        }
    }
}