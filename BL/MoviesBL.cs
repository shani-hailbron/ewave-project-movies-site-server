using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using DAL;
using System.Threading.Tasks;

namespace BL
{
    public class MoviesBL : IMoviesBL
    {
        IMoviesDL moviesDl;
        public MoviesBL(IMoviesDL _moviesDl)
        {
            moviesDl = _moviesDl;
        }


        public async Task<List<MoviesProps>> GetAllMoviesAsync()
        {
           return await moviesDl.GetAllMoviesAsync();
        }

        public async Task<int> AddMovieAsync(MoviesProps m)
        {
           return await moviesDl.AddMovieAsync(m);
        }

        public async Task DeleteMovieByIdAsync(int id)
        {
            await moviesDl.DeleteMovieByIdAsync(id);
        }

        public async Task<int> UpdateMovieAsync(int id, MoviesProps m)
        {
          return await moviesDl.UpdateMovieAsync(id, m);
        }
    }
}
