using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YourFavourites.Data;

namespace YourFavourites.Components
{
    public class MoviesListIncrementalView : IncrementalViewModel<Movie>
    {
        protected async override Task AddItems()
        {
            // Add the newly download data to the collection.
            IEnumerable < Movie > moviesCollection = await MoviesManager.GetMoviesManager().GetMovies(this.CurrentPosition, this.PageSize);

            foreach (Movie m in moviesCollection)
            {
              this.MyItems.Add(m);
            }
        }

        protected async override Task<bool> CheckMoreItems()
        {
            return await MoviesManager.GetMoviesManager().CountMovies() > this.CurrentPosition; 
        }
    }
}
