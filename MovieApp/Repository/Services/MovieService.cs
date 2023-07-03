using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;
using MovieApp.Repository.Interface;

namespace MovieApp.Repository.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;
        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Delete(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                Save();
                return true;
            }

            return false;
        }

        public List<Movie> GetAll()
        {
            return _context.Movies.ToList();

        }

        public PaginationViewModel<Movie> GetAllPaged(int pageNumber, int pageSize)
        {
            var movies = GetAll();
            var totalItems = movies.Count;

            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var items = movies.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new PaginationViewModel<Movie>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages

            };
            return viewModel;
        }


        public Movie GetById(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return null;
            }

            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return null;
            }
            return movie;

        }

        public bool Insert(Movie movie)
        {
            Guid guid = Guid.NewGuid();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//");
            if (movie.ImageFile != null)
            {
                string filepath = $"images//{guid}{movie.ImageFile.FileName}";

                var fullpath = Path.Combine(path, filepath);
                UploadFile(movie.ImageFile, fullpath);
                Movie model = new Movie()
                {
                    ImagePath = filepath,
                    MovieLink = movie.MovieLink,
                    Name = movie.Name,
                    ReleaseYear = movie.ReleaseYear,
                };
                _context.Movies.Add(model);
                Save();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Save()
        {
            _context.SaveChanges();

        }

        public bool Update(Movie movie)
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
                Save();
                return true;
            }
            else
            {
                return false;
            }
        }
        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);

        }
    }
}
