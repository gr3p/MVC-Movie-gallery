using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieGallery.Data;

namespace MovieGallery.Models.Helpers
{
    public static class ExtensionMethods
    {
        public static List<string> MapGengresToString(this Genre[] genres, MovieGalleryRepository movieRepo)
        {
            var resultList = new List<string>();
            foreach (var genreId in genres)
            {   
                resultList.Add(movieRepo.MovieGenre.Genres.FirstOrDefault(z => z.Id == genreId.id)?.Name);
            }

            return resultList;
        }
    }
}