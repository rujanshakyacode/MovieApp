using Microsoft.Build.Framework;
namespace MovieApp.ViewModel
{
    public class CreateMoviesVM
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        public string? MovieLink { get; set; }
        [Required]
        public IFormFile? ImagePath { get; set; }
    }
}

