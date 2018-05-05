using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourFavourites.Data;

namespace YourFavourites
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieDetails : ContentPage
	{
        readonly Movie currentMovie;

		public MovieDetails (Movie movie)
		{
            this.currentMovie = movie;

            BindingContext = movie;

            InitializeComponent ();
		}

        void OnAddFavClicked(Object sender, EventArgs e)
        {

        }

        private readonly string YouTubeUrl = "https://www.youtube.com/watch?v=";
        void OnShowTrailerClicked(Object sender, EventArgs e)
        {
            Device.OpenUri(new Uri(YouTubeUrl + this.currentMovie.ytid));
        }
    }

    // TODO: Check if the film is already a favourite one to update the button.
    // TODO: Renombrar clase.
    // TODO: Back pressed.
}