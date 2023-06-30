namespace MovieApp.Models
{
    internal class PaginationViewModel<T>
    {
        public List<Movie>? Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}