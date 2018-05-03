using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YourFavourites.Data
{
    public class MoviesManager
    {
        private const string baseUrl = "https://hydramovies.com/api-v2/?source=http://hydramovies.com/api-v2/current-Movie-Data.csv";

        public async Task<IEnumerable<Movie>> GetAll()
        {
            HttpClient httpClient = new HttpClient();
            string result = await httpClient.GetStringAsync(baseUrl);
            IEnumerable<Movie> movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(result);

            return movies;
        }
    }
}
