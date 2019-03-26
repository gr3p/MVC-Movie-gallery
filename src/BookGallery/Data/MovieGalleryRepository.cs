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

        private MovieGalleryRepository Repo { get; set; }
        public MovieGalleryRepository()
        {
            Repo = this;
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

        public Dictionary<int, MovieDetailsItem> GetDetailsAboutMovies(List<MovieItem> movies)
        {
            Dictionary<int, MovieDetailsItem> moviesDetailsItems = new Dictionary<int, MovieDetailsItem>();

            foreach (var movie in movies.Take(10))
            {
                
                var detailsForMovie = Repo.GetDetailsAboutMovie(movie.id);
                detailsForMovie.Credits = Repo.GetMovieCredits(movie.id);
                if (detailsForMovie.id != null) moviesDetailsItems.Add(detailsForMovie.id.Value, detailsForMovie);
            }

            return moviesDetailsItems;
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

        public CreditsModel GetMovieCredits(int movieId)
        {
            return API.MovieHttpClient.GetCreditsForMovie(movieId);
        }
    }
}