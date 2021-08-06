using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DAL
{
   public interface IMoviesDL
    {
        Task<List<MoviesProps>> GetAllMoviesAsync();
        Task DeleteMovieByIdAsync(int id);
        Task<int> AddMovieAsync(MoviesProps m);
        Task<int> UpdateMovieAsync(int id, MoviesProps m);

    }
}
