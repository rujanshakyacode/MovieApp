using Microsoft.Identity.Client;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ReleaseYear { get; set; }
        public string? MovieLink { get; set; }
        [DisplayName("ImagePicuture")]
        public string? ImagePath { get; set;}
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
