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
        protected async override Task AddItems()
        {
            IEnumerable<IElement> elementsCollection = null;
            elementsCollection = new FirebaseService().GetUserFavorites(AccountManager.GetAccountId());

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
