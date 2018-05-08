using AndroidAuthorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourFavourites.Components;
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

            BindingContext = new FirebaseService().GetUserFriends(AccountManager.GetAccountId());
			InitializeComponent ();
		}

        async void OnAddFriend(object sender, EventArgs e)
        {
            string email = friendsMail.Text;
            friendsMail.Text = "";
            string message = new FirebaseService().AddFriend(AccountManager.GetAccountId(),email.Replace('.',',')).Result;

            DisplayAlert("Notification", message, "OK");
            BindingContext = new FirebaseService().GetUserFriends(AccountManager.GetAccountId());

        }

        async void OnSelectedFriend(object sender, EventArgs e)
        {

        }
    }
}