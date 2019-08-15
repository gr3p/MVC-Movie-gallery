using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace APITests
{
    public class MovieSearchItems 
    {
        public int? Id { get; set; }
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        [JsonProperty(PropertyName = "Results")]
        public List<MovieItem> MovieItems { get; set; }
    }

    public class MovieItem
    {
        public int vote_count { get; set; }
        public int id { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public string title { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public int?[] genre_ids { get; set; }
        public List<string> GenreStrings { get; set; } = new List<string>();
        public string backdrop_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public ICollection<MovieSearchItems> MovieSearchItems { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ApiKey = "76b3c69a02263d0d7ff63b212d1e2c40";
              MovieSearchItems results;
            using (WebClient webc = new WebClient())
            {
                string date = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd"); ;
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
                    var y = serializ.Deserialize(jsonreader);
                    JObject parsed = JObject.Parse(y.ToString());

                    foreach (var pair in parsed)
                    {
                        Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                    }

                   
                }
            };

          
           

        }
    }
}
