using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MovieGallery.Models;

namespace MovieGallery.Context
{
    public class Context : DbContext
    {
        public Context()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        public DbSet<MovieSearchItems> movieSearchItems { get; set; }
        public DbSet<MovieItem> movieItem { get; set; }

    }
}