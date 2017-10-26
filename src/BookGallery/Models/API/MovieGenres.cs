using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace BookGallery.Models.API
{

    public class MovieGenres
    {
        [JsonProperty(PropertyName = "genres")]
        public Genre[] Genres { get; set; }
    }

    public class Genre
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }

}