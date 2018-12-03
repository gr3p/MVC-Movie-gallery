using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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
            ServicePointManager.ServerCertificateValidationCallback =
                delegate(object sender, X509Certificate certificate, X509Chain chain,
                    SslPolicyErrors sslPolicyErrors) { return true; };

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

        public MovieSearchItems GetRecentReleasedMovies()
        {
            return API.MovieHttpClient.GetRecentReleasedMovies();
        }
        public MovieSearchItems MoviesComingSoon()
        {
            return API.MovieHttpClient.GetMoviesComingSoon();
        }

        public MovieTrailers GetMovieTrailers(int movieId)
        {
            return API.MovieHttpClient.GetMovieTrailers(movieId);
        }
    }
}