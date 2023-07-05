using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess;
using MovieApp.DataAccess.Repository.Interface;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    public class UserMoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMovieService _movieService;

        public UserMoviesController(ApplicationDbContext context, IMovieService movieService)
        {
            _context = context;
            _movieService = movieService;
        }

        public IActionResult Index(int pageNumber, int pageSize)
        {
            var movies = _movieService.GetAllPaged(1, 4);

            return movies != null ? View(movies) : Problem("Something went Wrong");


        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }
    }
}

