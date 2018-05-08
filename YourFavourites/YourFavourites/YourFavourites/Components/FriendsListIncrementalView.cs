using AndroidAuthorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YourFavourites.Data;
using YourFavourites.Services;

namespace YourFavourites.Components
{
    public class FriendsListIncrementalView : IncrementalViewModel<User>
    {
        protected async override Task AddItems()
        {
            IEnumerable<User> usersCollection = null;

            usersCollection = await new FirebaseService().GetUserFriends(AccountManager.GetAccountId());

            foreach (User user in usersCollection)
            {
                this.MyItems.Add(user);
            }
        }

        protected async override Task<bool> CheckMoreItems()
        {
            return false;
        }
    }
}
