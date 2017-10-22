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
        public static MovieItem GetMovieResults(string searchFor)
        {
            var results = new MovieItem(); 
            var webc = new WebClient();
            var url = $"http://www.omdbapi.com/?t={searchFor}";
            //webc.Headers.Add("Ocp-Apim-Subscription-Key", "61bf0beaa35447b1b7f52aa3ae3b0556");
            var searchResult = webc.DownloadData(url);
            var serializ = new JsonSerializer();
            using (var stream = new MemoryStream(searchResult))
            using (var reader = new StreamReader(stream))

            using (var jsonreader = new JsonTextReader(reader))
            {
                results = serializ.Deserialize<MovieItem>(jsonreader);
            }
            return results;

        }

    }
}