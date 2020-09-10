using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieGallery.Models
{
    public class CreditsModel
    {
            public int? id { get; set; }
            public Cast[] cast { get; set; }
        

        public class Cast
        {
            public int cast_id { get; set; }
            public string character { get; set; }
            public string credit_id { get; set; }
            public int gender { get; set; }
            public int? id { get; set; }
            public string name { get; set; }
            public int order { get; set; }
            public string profile_path { get; set; }

            public bool HasProfileImage { get; set; }
        }

    }
}