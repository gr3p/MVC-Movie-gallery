using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieGallery.Data;
using MovieGallery.Models.API;

namespace MovieGallery.Models.ViewModels
{
    public class SearchMovieViewModel<T> : IMovieSearchItems
    {
        public API.Genre[] gengres { get; }
        public List<MovieItem> MovieItems { get; set; }
        public int page { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
       

        public SearchMovieViewModel(MovieGalleryRepository movieRepository, List<MovieItem> movieItems, int page, int totalPages, int totalResults)
        {
            gengres = movieRepository.MovieGenre.Genres;
            MovieItems = new List<MovieItem>(movieItems.ToList().OrderByDescending(d => d.release_date));
            this.page = page;
            this.total_pages = totalPages;
            this.total_results = totalResults;
          
            MapGengresStringToPage();
        }

   
        public void MapGengresStringToPage()
        {
            foreach (var movie in MovieItems)
            {
                foreach (var id in movie.genre_ids)
                {
                    movie.GenreStrings.Add(gengres.FirstOrDefault(z => z.Id == id)?.Name);
                }
            }
        }
      
    }
}