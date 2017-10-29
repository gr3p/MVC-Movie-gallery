namespace MovieGallery.Models
{
    public interface IMovieSearchItems
    {
        MovieItem[] MovieItems { get; set; }
        int page { get; set; }
        int total_pages { get; set; }
        int total_results { get; set; }
    }
}