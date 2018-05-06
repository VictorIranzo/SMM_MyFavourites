using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YourFavourites.Data;

namespace YourFavourites.Components
{
    public class MoviesListIncrementalView : IncrementalViewModel<Movie>
    {
        public enum TypeLoad
        {
            NO_FILTER,
            FILTER_BY_TITLE
        }

        private TypeLoad typeLoad;
        private string filter;

        private MoviesListIncrementalView() { }

        public MoviesListIncrementalView(TypeLoad typeLoad, string filter = "")
            :base()
        {
            this.typeLoad = typeLoad;
            this.filter = filter;
        }

        protected async override Task AddItems()
        {
            IEnumerable<Movie> moviesCollection = null;

            // Add the newly download data to the collection.
            switch (this.typeLoad)
            {
                case TypeLoad.FILTER_BY_TITLE:
                    moviesCollection = await MoviesManager.GetMoviesManager().FilterByTitle(this.filter, this.CurrentPosition, this.PageSize);
                    break;

                case TypeLoad.NO_FILTER:
                default:
                    moviesCollection = await MoviesManager.GetMoviesManager().GetMovies(this.CurrentPosition, this.PageSize);
                    break;
            }

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
