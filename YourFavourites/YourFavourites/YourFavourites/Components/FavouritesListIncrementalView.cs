using AndroidAuthorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YourFavourites.Data;
using YourFavourites.Services;

namespace YourFavourites.Components
{
    public class FavouritesListIncrementalView : IncrementalViewModel<IElement>
    {
        private string user_id;

        private FavouritesListIncrementalView() { }

        public FavouritesListIncrementalView(string user_id)
        {
            this.user_id = user_id;
        }

        protected async override Task AddItems()
        {
            IEnumerable<IElement> elementsCollection = null;
            elementsCollection = new FirebaseService().GetUserFavorites(this.CurrentPosition, this.PageSize, user_id);

            foreach (IElement element in elementsCollection)
            {
                this.MyItems.Add(element);
            }
        }

        protected async override Task<bool> CheckMoreItems()
        {
            return this.MyItems.Count > this.CurrentPosition;
        }
    }
}
