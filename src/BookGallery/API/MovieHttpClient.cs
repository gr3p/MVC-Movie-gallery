﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices.CompensatingResourceManager;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using MovieGallery.Models;
using MovieGallery.Models;
using MovieGallery.Models.API;
using Newtonsoft.Json;
using ServiceStack.Text;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace MovieGallery.API
{
    public class MovieHttpClient
    {
        private static string ApiKey => System.Configuration.ConfigurationManager.AppSettings["API-key"];

        public static HttpClient HttpClient { get; set; } = new HttpClient();

        /// <summary>
        /// API Resources here: https://developers.themoviedb.org/ och https://www.themoviedb.org/documentation/api
        ///
        /// </summary>
        /// <param name="searchFor"></param>
        /// <returns></returns>
       
        public static async Task<MovieSearchItems> GetMovieResults(string searchFor)
        {
            var url =
                    $"https://api.themoviedb.org/3/search/movie?api_key={ApiKey}&language=en-US&query={searchFor}&page=1&include_adult=false";

            return await DownloadDataFromApiAsync<MovieSearchItems>(url);
        }

        public static async Task<MovieDetailsItem> GetDetailedMovieResults(int movieId)
        {
            var url = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={ApiKey}&language=en-US";
            return await DownloadDataFromApiAsync<MovieDetailsItem>(url);
        }

        public static async Task<MovieReleaseDate> GetMovieReleaseDates(int movieId)
        {
            var url = $"https://api.themoviedb.org/3/movie/{movieId}/release_dates?api_key={ApiKey}";
            return await DownloadDataFromApiAsync<MovieReleaseDate>(url);
        }

        public static  MovieGenres GetMovieGenres()
        {
            var url = $"http://api.themoviedb.org/3/genre/movie/list?api_key={ApiKey}";
            return  DownloadDataFromApi<MovieGenres>(url);
        }

        public static async Task<MovieSearchItems> GetPopularMovies()
        {
            var url = $"https://api.themoviedb.org/3/movie/popular?api_key={ApiKey}&language=en-US&page=1";
            return await DownloadDataFromApiAsync<MovieSearchItems>(url);
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

            var webc = new WebClient();
            var searchResult = webc.DownloadData(url);
            webc.Dispose();
            var jsonSerializer = new JsonSerializer();
            using (var stream = new MemoryStream(searchResult))
            using (var reader = new StreamReader(stream))

            using (var jsonReader = new JsonTextReader(reader))
            {
                results = jsonSerializer.Deserialize<MovieSearchItems>(jsonReader);
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

            var webc = new WebClient();
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
                 $"https://api.themoviedb.org/3/movie/{movieId}/credits?api_key={ApiKey}");
        }
        public static ActorResultItem GetActors(string actorToFind, string pageIndex)
        {
            return DownloadDataFromApi<ActorResultItem>(
                $"https://api.themoviedb.org/3/search/person?api_key={ApiKey}&language=en-US&query={actorToFind}&page={pageIndex}&include_adult=false");
        }

        public static ActorDetails GetActorDetails(int actorId)
        {
            return DownloadDataFromApi<ActorDetails>(
                $"https://api.themoviedb.org/3/person/{actorId}?api_key={ApiKey}&language=en-US");
        }

        public static T DownloadDataFromApi<T>(string apiUrl) where T : class
        {
            T results;

            var url = $"{apiUrl}";

            var webc = new WebClient();
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

        public static async Task<T> DownloadDataFromApiAsync<T>(string url) where T : class
        {
            T results = default(T);
            
            try
            {
                HttpResponseMessage response = HttpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Request Message Information:- \n\n" + response.RequestMessage + "\n");
                    Debug.WriteLine("Response Message Header \n\n" + response.Content.Headers + "\n");

                    var customerJsonString = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Your response data is: " + customerJsonString);

                    results = JsonConvert.DeserializeObject<T>(customerJsonString);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("The Error:" + e);
            }

            return results;
        }
        public HttpResponseMessage ReturnBytes(byte[] bytes)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(bytes);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }

    }
}