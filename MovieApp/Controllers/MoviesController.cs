using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using MovieApp.Data;
using MovieApp.Models;
using System.Drawing.Printing;


namespace MovieApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            var movies = await _context.Movies.ToListAsync(); // Get all products from the service
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


            return viewModel != null ?
                        View(viewModel) :
                        Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie model)
        {
            if (ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//");
                if (model.ImageFile != null)
                {
                    string filepath = $"images//{guid}{model.ImageFile.FileName}";

                    var fullpath = Path.Combine(path, filepath);
                    UploadFile(model.ImageFile, fullpath);
                    Movie movie = new Movie()
                    {
                        ImagePath = filepath,
                        MovieLink = model.MovieLink,
                        Name = model.Name,
                        ReleaseYear = model.ReleaseYear,
                    };
                    _context.Movies.Add(movie);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);

        }

        // GET: Movies/Edit/5
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

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ReleaseYear,MovieLink,ImagePath,ImageFile")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Guid guid = Guid.NewGuid();
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//");
                    if (movie.ImageFile != null)
                    {

                        var filepath = "images//" + guid + movie.ImageFile.FileName;
                        var fullpath = Path.Combine(path, filepath);
                        UploadFile(movie.ImageFile, fullpath);
                        movie.ImagePath = filepath;
                        _context.Update(movie);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }



    }
}
