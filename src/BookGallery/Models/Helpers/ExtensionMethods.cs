using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MovieGallery.Data;

namespace MovieGallery.Models.Helpers
{
    public static class ExtensionMethods
    {
        public static MvcHtmlString MapGenresToHtmlString(
            this Genre[] genres,
            MovieGalleryRepository movieRepo)
        {
            var sb = new StringBuilder();

            foreach (var genre in genres)
            {
                var genreName = movieRepo.MovieGenre
                    .Genres
                    .FirstOrDefault(z => z.Id == genre.id)?.Name;

                if (!string.IsNullOrEmpty(genreName))
                {
                    sb.Append($"<span class=\"genre-badge\">{HttpUtility.HtmlEncode(genreName)}</span>");
                }
            }

            return new MvcHtmlString(sb.ToString());
        }
        public static int MapPopularityToStars(this float popularity)
        {
            float minPopularity = 10f;  
            float maxPopularity = 130f; 
            // Normalisera värdet mellan 0 och 1
            float normalized = (popularity - minPopularity) / (maxPopularity - minPopularity);
            normalized = Math.Max(0f, Math.Min(normalized, 1f));
            
            int stars = (int)Math.Round(normalized * 5);
            return Math.Max(1, stars);
        }

        
        public static MvcHtmlString RenderPopularityStars(this float popularity)
        {
            int stars = popularity.MapPopularityToStars();
            var starHtml = string.Empty;
            for (int i = 0; i < stars; i++)
            {
                starHtml += "<i class=\"fa fa-star green-star\"></i>";
            }
            return new MvcHtmlString(starHtml);
        }
    }
}
