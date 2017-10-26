using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookGallery.Data;
using BookGallery.Models.API;

namespace BookGallery.Models.ViewModels
{
    public class SearchMovieViewModel<T> : IMovieSearchItems
    {
        public Genre[] gengres { get; }
        public MovieItem[] MovieItems { get; set; }
        public int page { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
       
        public SearchMovieViewModel(MovieGalleryRepository movieRepository, MovieItem[] movieItems, int page, int total_pages, int total_results)
        {
            gengres = movieRepository.MovieGenre.Genres;
            MovieItems = movieItems;
            this.page = page;
            this.total_pages = total_pages;
            this.total_results = total_results;
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