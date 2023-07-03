using MovieApp.Models;

namespace MovieApp.Repository.Interface
{
    public interface IMovieService

    {
        List<Movie> GetAll();
        PaginationViewModel<Movie> GetAllPaged(int pageNumber, int pageSize);
        Movie GetById(int? id);
        bool Insert(Movie movie);
        bool Update(Movie movie);
        bool Delete(int id);
        void Save();



    }
}
