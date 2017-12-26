using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MovieGallery.Data;
using MovieGallery.Models;

namespace MovieGallery.Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            var localMovies = new LocalMovies();
            var localdirs = localMovies.GetMoviesfromDirList();
            var lmovies = localMovies.CleanReleaseNames(localdirs);

            MovieGalleryRepository mrepo = new MovieGalleryRepository();
            foreach (var movie in lmovies)
            {
             var searchResult = mrepo.SearchForAMovie(movie);
             context.movieSearchItems.Add(searchResult);
            }

            context.SaveChanges();
        }
    }
}