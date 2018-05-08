using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YourFavourites
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FriendsPage : ContentPage
	{
		public FriendsPage ()
		{
			InitializeComponent ();
		}

        async void OnAddFriend(object sender, EventArgs e)
        {

        }

        async void OnSelectedFriend(object sender, EventArgs e)
        {

        }
    }
}