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
	}

    // TODO: Renombrar clase.
    // TODO: Back pressed.
}