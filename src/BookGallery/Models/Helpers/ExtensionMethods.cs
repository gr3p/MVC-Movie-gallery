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
    }
}