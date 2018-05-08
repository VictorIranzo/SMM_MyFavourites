using AndroidAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourFavourites.Data;
using YourFavourites.Services;

namespace YourFavourites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;

            CheckIfUserExists();
        }

        private void CheckIfUserExists()
        {
            FirebaseService firebaseService = new FirebaseService();

            User user = new User()
            {
                Id = AccountManager.GetAccountId(),
                Email = AccountManager.GetAccountMail().Replace('.', ','),
                Name = AccountManager.GetAccountName(),
                UrlImage = AccountManager.GetImageURL()
            };

            bool existsUser = firebaseService.CheckUserExists(user).Result;

            if (!existsUser)
            {
                firebaseService.AddUser(user);
            }
        }

        public void SetDetailPage(Page page)
        {
            Detail = new NavigationPage(page);
            IsPresented = false;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageMenuItem;
            if (item == null)
                return;

            Page page = null;

            switch (item.Title)
            {
                case "Favourites":
                    page = new FavouritesPage(this);
                    break;
                case "Movies":
                    page = new MoviesPage(this);
                    break;
                case "Songs":
                    page = new SongsPage(this);
                    break;
                case "Books":
                    page = new BooksPage(this);
                    break;
                case "Friends":
                    page = new FriendsPage(this);
                    break;
                case "Log out":
                    AccountManager.LogOut();
                    return;
            }

            page.Title = item.Title;

            SetDetailPage(page);

            MasterPage.ListView.SelectedItem = null;
        }
    }
}