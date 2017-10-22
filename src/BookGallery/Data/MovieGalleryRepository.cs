using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using BookGallery.Models;

namespace BookGallery.Data
{
    public class MovieGalleryRepository
    {

        public MovieGalleryRepository()
        {
            
            
        }

        public MovieItem SearchForAMovie(string SearchText)
        {
            return API.MovieHttpClient.GetMovieResults(SearchText);

        }
    }
}