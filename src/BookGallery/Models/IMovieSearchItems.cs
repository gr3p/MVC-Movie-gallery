using System.Collections.Generic;
using System.Linq;

namespace MovieGallery.Models
{
    public interface IMovieSearchItems
    {
        List<MovieItem> MovieItems { get; set; }
        int page { get; set; }
        int total_pages { get; set; }
        int total_results { get; set; }
    }
}