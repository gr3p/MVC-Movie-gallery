using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace BookGallery.Models
{
    public class BookGallery
    {
        public int Id { get; set; }
        public string SeriesTitle { get; set; }
        public int IssueNumber { get; set; }
        public string DescriptionHtml { get; set; }
        public Artist[] Artists { get; set; }
        public bool Favorite { get; set; }

        public string DisplayText
        {
            get { return SeriesTitle + " #" + IssueNumber; }
        }
        
        //Images Scheme: series-title-issuenumber.jpg
        public string CoverImageFileName
        {
            get { return SeriesTitle.Replace(" ", "-").ToLower() + "-" + IssueNumber + ".jpg"; }
        }

    }
}