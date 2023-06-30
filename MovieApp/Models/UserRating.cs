using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApp.Models
{
    public class UserRating
    {
        [ForeignKey("movies")]
        public int MovieId { get; set; }
        public Movie? movies { get; set; }
        public int UserId { get; set; }

    }
}
