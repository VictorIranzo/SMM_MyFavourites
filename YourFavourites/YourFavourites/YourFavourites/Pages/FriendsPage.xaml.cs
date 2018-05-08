using AndroidAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourFavourites.Components;
using YourFavourites.Data;
using YourFavourites.Services;

namespace YourFavourites
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FriendsPage : ContentPage, BackablePage
	{
        public MainPage MainPage { get; set; }

        public FriendsPage (MainPage mainPage)
		{
            this.MainPage = mainPage;

            BindingContext = new FriendsListIncrementalView();
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FriendsListIncrementalView vm = BindingContext as FriendsListIncrementalView;

            vm.LoadMoreItemsCommand.Execute(null);
        }

        async void OnAddFriend(object sender, EventArgs e)
        {
            string email = friendsMail.Text;
            friendsMail.Text = "";
            string message = new FirebaseService().AddFriend(AccountManager.GetAccountId(),email.Replace('.',',')).Result;

            DisplayAlert("Notification", message, "OK");
            FriendsListIncrementalView friendsViewIncrementalView = new FriendsListIncrementalView();
            BindingContext = friendsViewIncrementalView;

            friendsViewIncrementalView.LoadMoreItemsCommand.Execute(null);
        }

        async void OnSelectedFriend(object sender, ItemTappedEventArgs e)
        {
            User u = (User)e.Item;
            Page page = new FavouritesPage(this.MainPage, u.Id);
            page.Title = u.Name;

            this.MainPage.SetDetailPage(page);
        }
    }
}