using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using MovieGallery.Models;
using MovieGallery.Models;
using MovieGallery.Models.API;
using Newtonsoft.Json;

namespace MovieGallery.API
{
    public class MovieHttpClient
    {
        private static string ApiKey => "76b3c69a02263d0d7ff63b212d1e2c40";

        private static WebClient webc = new WebClient();
        /// <summary>
        /// API Resources here: https://developers.themoviedb.org/ och https://www.themoviedb.org/documentation/api
        /// API KEY: 76b3c69a02263d0d7ff63b212d1e2c40
        /// </summary>
        /// <param name="searchFor"></param>
        /// <returns></returns>
        /// https://api.themoviedb.org/3/search/movie?api_key=76b3c69a02263d0d7ff63b212d1e2c40&language=en-US&query=coco&page=1&include_adult=false
        public static MovieSearchItems GetMovieResults(string searchFor)
        {
            using (WebClient webc = new WebClient())
            {
                var url =
                    $"https://api.themoviedb.org/3/search/movie?api_key={ApiKey}&language=en-US&query={searchFor}&page=1&include_adult=false";

                return DownloadDataFromApi<MovieSearchItems>(url);
            }
        }

        public static MovieDetailsItem GetDetailedMovieResults(int movieId)
        {
            var url = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={ApiKey}&language=en-US";
            return DownloadDataFromApi<MovieDetailsItem>(url);
        }

        public static MovieReleaseDate GetMovieReleaseDates(int movieId)
        {
            var url = $"https://api.themoviedb.org/3/movie/{movieId}/release_dates?api_key={ApiKey}";
            return DownloadDataFromApi<MovieReleaseDate>(url);
        }

        public static MovieGenres GetMovieGenres()
        {
            var url = $"http://api.themoviedb.org/3/genre/movie/list?api_key={ApiKey}";
            return DownloadDataFromApi<MovieGenres>(url);
        }

        public static MovieSearchItems GetPopularMovies()
        {
            var url = $"https://api.themoviedb.org/3/movie/popular?api_key={ApiKey}&language=en-US&page=1";
            return DownloadDataFromApi<MovieSearchItems>(url);

        }

        public static MovieSearchItems GetRecentReleasedMovies()
        {
            MovieSearchItems results;
            
                string date = DateTime.Today.AddMonths(-2).ToString("yyyy-MM-dd"); ;
                var url = $"https://api.themoviedb.org/3/discover/movie?" +
                          $"sort_by=popularity.desc" +
                          $"&api_key={ApiKey}" +
                          $"&release_date.gte={date}" +
                          $"&release_date.lte={DateTime.Today:yyyy-MM-dd}" +
                          $"&with_release_type=5";


                var searchResult = webc.DownloadData(url);
                webc.Dispose();
                var serializ = new JsonSerializer();
                using (var stream = new MemoryStream(searchResult))
                using (var reader = new StreamReader(stream))

                using (var jsonreader = new JsonTextReader(reader))
                {

                    results = serializ.Deserialize<MovieSearchItems>(jsonreader);
                }
            
            return results;

        }

        public static MovieSearchItems GetMoviesComingSoon()
        {
            MovieSearchItems results;
            
                string tommorow = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"); ;
                var url = $"https://api.themoviedb.org/3/discover/movie?" +
                          $"sort_by=popularity.desc" +
                          $"&api_key={ApiKey}" +
                          $"&release_date.gte={tommorow}" +
                          $"&with_release_type=5";

                var searchResult = webc.DownloadData(url);
                webc.Dispose();
                var serializ = new JsonSerializer();
                using (var stream = new MemoryStream(searchResult))
                using (var reader = new StreamReader(stream))

                using (var jsonreader = new JsonTextReader(reader))
                {
                    results = serializ.Deserialize<MovieSearchItems>(jsonreader);
                }
            
            return results;

        }

        public static MovieTrailers GetMovieTrailers(int movieId)
        {
            var url = $"https://api.themoviedb.org/3/movie/{movieId}/videos?api_key={ApiKey}&language=en-US";
            return DownloadDataFromApi<MovieTrailers>(url);
        }

        public static CreditsModel GetCreditsForMovie(int movieId)
        {
            return DownloadDataFromApi<CreditsModel>(
                 $"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key=76b3c69a02263d0d7ff63b212d1e2c40");
        }
        public static ActorResultItem GetActors(string actorToFind, string pageIndex)
        {
            return DownloadDataFromApi<ActorResultItem>(
                $"https://api.themoviedb.org/3/search/person?api_key=76b3c69a02263d0d7ff63b212d1e2c40&language=en-US&query={actorToFind}&page={pageIndex}&include_adult=false");
        }
        
        public static ActorDetails GetActorDetails(int actorId)
        {
            return DownloadDataFromApi<ActorDetails>(
                $"https://api.themoviedb.org/3/person/{actorId}?api_key=76b3c69a02263d0d7ff63b212d1e2c40&language=en-US");
        }

        public static T DownloadDataFromApi<T>(string apiUrl) where T : class
        {
            T results;
            
                var url = $"{apiUrl}";

                var searchResult = webc.DownloadData(url);
                webc.Dispose();
                var jsonSerializer = new JsonSerializer();
                using (var stream = new MemoryStream(searchResult))
                using (var reader = new StreamReader(stream))

                using (var jsonTextReader = new JsonTextReader(reader))
                {
                    results = jsonSerializer.Deserialize<T>(jsonTextReader);
                }
                
            return results;

        }


        ///DVD : release_dates
        //157336 Interstellar
        //354912 COCO new.
        //http://api.themoviedb.org/3/movie/157336/videos?api_key=76b3c69a02263d0d7ff63b212d1e2c40
        //https://www.youtube.com/watch?v=ePbKGoIGAXY
        //https://www.themoviedb.org/talk/5451ec02c3a3680245005e3c

        //Release Date for coco to dvd?
        //http://api.themoviedb.org/3/movie/354912/release_dates?api_key=76b3c69a02263d0d7ff63b212d1e2c40

       
    }
}