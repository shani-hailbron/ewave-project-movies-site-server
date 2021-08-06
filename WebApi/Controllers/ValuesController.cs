using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BL;
using Entities;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        IMoviesBL moviesBl;
        public ValuesController(IMoviesBL _moviesBl)
        {
            moviesBl = _moviesBl;
        }

        [HttpGet("GetAllMovies")]
        public async Task<IEnumerable<MoviesProps>> GetAllMovies()
        {
            return await moviesBl.GetAllMoviesAsync();
        }

        [HttpPost("AddNewMovie")]
        public async Task<int> AddNewMovie([FromBody] MoviesProps mp)
        {
           return await moviesBl.AddMovieAsync(mp);
        }

        [HttpDelete("DeleteMovieById/{id}")]
        public async Task DeleteMovieById(int id)
        {
           await moviesBl.DeleteMovieByIdAsync(id);
        }


        [HttpPut("UpdateMovie/{id}")]
        public async Task<int> UpdateMovie(int id, [FromBody] MoviesProps mp)
        {
          return await  moviesBl.UpdateMovieAsync(id, mp);
        }


    }
}
