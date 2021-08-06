using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace DAL
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.HostingEnvironment = env;
            this.Configuration = configuration;
        }
    }

    public class MoviesDL : IMoviesDL
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private string webRoot;
        private string path;

        public MoviesDL(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            webRoot = _hostingEnvironment.WebRootPath;
            path = System.IO.Path.Combine(webRoot, "MoviesDetails.json");
        }

        //return all movies
        public async Task<List<MoviesProps>> GetAllMoviesDetails()
        {
            try
            {
                List<MoviesProps> items;
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<MoviesProps>>(json);
                }
                return  items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task<List<MoviesProps>> GetAllMoviesAsync()
        {
            return await GetAllMoviesDetails();
        }

        //deleting a movie
        public async Task DeleteMovieByIdAsync(int id)
        {
            List<MoviesProps> items = await GetAllMoviesDetails();
            try
            {
                var movie = items.Where(i => i.Id == id).First();
                items.Remove(movie);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(path, output);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //adding a new movie
        public async Task<int> AddMovieAsync(MoviesProps m)
        {
            List<MoviesProps> items = await GetAllMoviesDetails(); 
            try {
                var flag = items.FirstOrDefault(i => i.MovieName == m.MovieName);
                if (flag == null)
                {
                    if (items.Count < 10)
                    {
                        items.Add(m);
                        string output = Newtonsoft.Json.JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
                        System.IO.File.WriteAllText(path, output);
                    }
                    else
                    {
                        int minRating = items.Min(r => r.Rating);
                        var lessRating = items.First(i => i.Rating == minRating);
                        items.Remove(lessRating);
                        items.Add(m);
                        string output = Newtonsoft.Json.JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
                        System.IO.File.WriteAllText(path, output);
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return 1;
    }

        //update movie
        public async Task<int> UpdateMovieAsync(int id, MoviesProps m)
        {
            try
            {
                List<MoviesProps> items = await GetAllMoviesDetails();
                var flag = items.FirstOrDefault(i => i.MovieName == m.MovieName);
                if (flag == null || flag.Id==m.Id) { 
                    int currentMovieIndex = items.FindIndex(i => i.Id == id);
                    MoviesProps currentMovie = items.First(i => i.Id == id);
                    currentMovie.MovieName = m.MovieName;
                    currentMovie.Rating = m.Rating;
                    items[currentMovieIndex] = currentMovie;
                    currentMovie.Category = m.Category;
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
                    System.IO.File.WriteAllText(path, output);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return 1;
        }
    }
}
