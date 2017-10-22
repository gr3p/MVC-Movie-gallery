using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using BookGallery.Models;
using Newtonsoft.Json;

namespace BookGallery.API
{
    public class MovieHttpClient
    {
        public static MovieSearchItems GetMovieResults(string searchFor)
        {
            var results = new MovieSearchItems(); 
            var webc = new WebClient();
            var url = $"https://api.themoviedb.org/3/search/movie?api_key=76b3c69a02263d0d7ff63b212d1e2c40&language=en-US&query={searchFor}&page=1&include_adult=false";
         
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