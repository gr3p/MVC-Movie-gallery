using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using BookGallery.Models;
using BookGallery.Models.API;
using Newtonsoft.Json;

namespace BookGallery.API
{
    public class MovieHttpClient
    {
        private static string ApiKey => "76b3c69a02263d0d7ff63b212d1e2c40";
        /// <summary>
        /// API Resources here: https://developers.themoviedb.org/ och https://www.themoviedb.org/documentation/api
        /// API KEY: 76b3c69a02263d0d7ff63b212d1e2c40
        /// </summary>
        /// <param name="searchFor"></param>
        /// <returns></returns>
        public static MovieSearchItems GetMovieResults(string searchFor)
        {
            var results = new MovieSearchItems(); 
            var webc = new WebClient();
            var url = $"https://api.themoviedb.org/3/search/movie?api_key={ApiKey}&language=en-US&query={searchFor}&page=1&include_adult=false";
                
            var searchResult = webc.DownloadData(url);
            var serializ = new JsonSerializer();
            using (var stream = new MemoryStream(searchResult))
            using (var reader = new StreamReader(stream))

            using (var jsonreader = new JsonTextReader(reader))
            {
                results = serializ.Deserialize<MovieSearchItems>(jsonreader);
            }
            return results;

        }

        public static MovieGenres GetMovieGenres()
        {
            var genres = new MovieGenres();
            var webc = new WebClient();
            var url = $"http://api.themoviedb.org/3/genre/movie/list?api_key={ApiKey}";

            var searchResult = webc.DownloadData(url);
            var serializ = new JsonSerializer();
            using (var stream = new MemoryStream(searchResult))
            using (var reader = new StreamReader(stream))

            using (var jsonreader = new JsonTextReader(reader))
            {
                genres = serializ.Deserialize<MovieGenres>(jsonreader);
            }
            return genres;

        }

        public static MovieSearchItems GetPopularMovies()
        {
            var results = new MovieSearchItems();
            var webc = new WebClient();
            var url = $"https://api.themoviedb.org/3/movie/popular?api_key={ApiKey}&language=en-US&page=1";

            var searchResult = webc.DownloadData(url);
            var serializ = new JsonSerializer();
            using (var stream = new MemoryStream(searchResult))
            using (var reader = new StreamReader(stream))

            using (var jsonreader = new JsonTextReader(reader))
            {
                results = serializ.Deserialize<MovieSearchItems>(jsonreader);
            }
            return results;

        }




    }
}