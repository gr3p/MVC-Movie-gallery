using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
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
                delegate (object sender, X509Certificate certificate, X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                { return true; };

            GetMovieGenres();
        }

        private void GetMovieGenres()
        {
            MovieGenre = API.MovieHttpClient.GetMovieGenres();
        }

        public MovieSearchItems SearchForAMovie(string SearchText)
        {
            return API.MovieHttpClient.GetMovieResults(SearchText).Result;

        }
        public async Task<MovieDetailsItem> GetDetailsAboutMovie(int movieId)
        {
            return await API.MovieHttpClient.GetDetailedMovieResults(movieId);

        }



        public Task<MovieSearchItems> GetMostPopularMovies()
        {
            return API.MovieHttpClient.GetPopularMovies();
        }

        public Release_Dates GetBlueRayReleaseDate(int movieId)
        {
            var result = API.MovieHttpClient.GetMovieReleaseDates(movieId).Result;
            var releaseDates = result.results.Where(x => x.iso_3166_1 == "US").Select(x => x.release_dates).ToList()
                .FirstOrDefault();
            return releaseDates?.Where(x => x.type == 5).FirstOrDefault();
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

        public async Task<CreditsModel> GetMovieCredits(int movieId)
        {
            var credits = API.MovieHttpClient.GetCreditsForMovie(movieId);
            var imageValidatedCredits = await ActorImageExistValidator(credits);

            return imageValidatedCredits;
        }


        private async Task<CreditsModel> ActorImageExistValidator(CreditsModel credits, string Url = "http://image.tmdb.org/t/p/w185")
        {
            List<Task<CreditsModel.Cast>> tasks = new List<Task<CreditsModel.Cast>>();
            foreach (var cast in credits.cast)
            {
                
               tasks.Add(Download(cast, Url));
               
            }
            credits.cast = await Task.WhenAll(tasks);
            return credits;


        }
        public HttpClient HttpClient { get; set; } = new HttpClient();
        


        private async Task<CreditsModel.Cast> Download(CreditsModel.Cast cast, string url)
        {
            var imageLookupURL = $"{url}{cast.profile_path}";
            try
            {
                //var request = await HttpClient.GetAsync(imageLookupURL, HttpCompletionOption.ResponseHeadersRead);

                var result = HttpClient.GetAsync(imageLookupURL, HttpCompletionOption.ResponseHeadersRead);
                var response = result.Result;
                cast.HasProfileImage = response.StatusCode == HttpStatusCode.OK;
               
            }
            catch (Exception ex)
            {
                //TODO
            }

            return cast;
        }

        public ActorResultItem SearchForActors(string actorToFind, string pageIndex = "1")
        {
            return API.MovieHttpClient.GetActors(actorToFind, pageIndex);
        }
        public ActorDetails SearchForActorDetails(int actorId)
        {
            return API.MovieHttpClient.GetActorDetails(actorId);
        }
    }
}