using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MovieGallery.Models
{

    public class ActorResultItem
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        [JsonProperty(PropertyName = "Results")]
        public ActorResult[] results { get; set; }
        
    }

    public class ActorResult
    {
        public float popularity { get; set; }
        public string known_for_department { get; set; }
        public string name { get; set; }
        public int id { get; set; }
        public string profile_path { get; set; }
        public bool adult { get; set; }
        public KnownFor[] known_for { get; set; }
        public int gender { get; set; }
        public ActorDetails ActorDetails { get; set; }
    }

    public class KnownFor
    {
        public int vote_count { get; set; }
        public int id { get; set; }
        public bool video { get; set; }
        public string media_type { get; set; }
        public float vote_average { get; set; }
        public string title { get; set; }
        public string release_date { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public int[] genre_ids { get; set; }
        public string backdrop_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
        public string original_name { get; set; }
        public string name { get; set; }
        public string first_air_date { get; set; }
        public string[] origin_country { get; set; }
    }


    public class ActorDetails
    {
        public string birthday { get; set; }
        public string known_for_department { get; set; }
        public string deathday { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string[] also_known_as { get; set; }
        public int gender { get; set; }
        public string biography { get; set; }
        public float popularity { get; set; }
        public string place_of_birth { get; set; }
        public string profile_path { get; set; }
        public bool adult { get; set; }
        public string imdb_id { get; set; }
        public object homepage { get; set; }
    }


}
