using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YourFavourites.Data
{
    public class MoviesManager
    {
        private const string baseUrl = "https://hydramovies.com/api-v2/?source=http://hydramovies.com/api-v2/current-Movie-Data.csv&sort=imdb_rating";

        private IEnumerable<Movie> movies;

        private static MoviesManager instance;

        public static MoviesManager GetMoviesManager() {
            if (instance == null) instance = new MoviesManager();

            return instance;
        }

        private MoviesManager() { }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            if (movies == null)
            {
                await GetMovies();
            }

            return movies;
        }

        public async Task<IEnumerable<Movie>> GetMovies(int offset, int count)
        {
            if (movies == null)
            {
                await GetMovies();
            }

            return movies.Skip(offset).Take(count);
        }

        public async Task<int> CountMovies()
        {
            if (movies == null)
            {
                await GetMovies();
            }

            return movies.Count();
        }

        public async Task<IEnumerable<Movie>> FilterByTitle(string title, int offset, int count)
        {
            return movies.Where(m => m.Title != null && m.Title.ToLower().Contains(title)).Skip(offset).Take(count);
        }

        private async Task GetMovies()
        {
            HttpClient httpClient = new HttpClient();
            string result = await httpClient.GetStringAsync(baseUrl);
            movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(result);
        }
    }
}
