using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MovieGallery.Models
{

        public class MovieReleaseDate
        {
            public int id { get; set; }

        [JsonProperty(PropertyName = "Results")]
        public Result[] results { get; set; }
        }

        public class Result
        {
            public string iso_3166_1 { get; set; }
            public Release_Dates[] release_dates { get; set; }
        }

        public class Release_Dates
        {
            public string certification { get; set; }
            public string iso_639_1 { get; set; }
            public string note { get; set; }
            public DateTime release_date { get; set; }
            public int type { get; set; }
        }

    
}