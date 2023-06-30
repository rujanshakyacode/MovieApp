using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    public class UserMoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserMoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 4)
        {
            var movies = await _context.Movies.ToListAsync();// Get all products from the service
            var totalItems = movies.Count();

            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var items = movies.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new PaginationViewModel<Movie>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };

            return viewModel != null ? View(viewModel) : Problem("Something went Wrong");


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

