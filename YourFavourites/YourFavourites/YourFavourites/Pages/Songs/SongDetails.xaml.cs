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
	public partial class SongDetails : ContentPage
	{
        readonly SongsPage songsPage;
        readonly Song currentSong;
        bool IsFavourite;

        public SongDetails(Song song, SongsPage songsPage)
        {
            this.currentSong = song;
            this.songsPage = songsPage;

            BindingContext = song;

            FirebaseService firebaseService = new FirebaseService();

            IsFavourite = firebaseService.CheckIfIsFavourite(AccountManager.GetAccountId(), currentSong).Result;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetFavButton();
        }

        void OnAddFavClicked(Object sender, EventArgs e)
        {
            FirebaseService firebaseService = new FirebaseService();

            if (IsFavourite)
            {
                firebaseService.RemoveFavourite(AccountManager.GetAccountId(), currentSong);
                IsFavourite = false;
            }
            else
            {
                firebaseService.AddFavourite(AccountManager.GetAccountId(), currentSong);
                IsFavourite = true;
            }

            SetFavButton();
        }

        private void SetFavButton()
        {
            if (IsFavourite) butAddFavourite.Text = "Remove from favourites";
            else butAddFavourite.Text = "Add to favourites";
        }

        protected override bool OnBackButtonPressed()
        {
            this.songsPage.mainPage.SetDetailPage(this.songsPage);

            return true;
        }

        void OnListenSongClicked(Object sender, EventArgs e)
        {
            YouTubeService.OpenFirstYouTubeVide(this.currentSong.MainTitle);
        }

    }
}