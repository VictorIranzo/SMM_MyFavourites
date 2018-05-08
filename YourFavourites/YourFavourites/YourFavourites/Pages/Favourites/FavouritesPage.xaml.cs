using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourFavourites.Components;
using YourFavourites.Data;

namespace YourFavourites
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavouritesPage : BackablePage
	{
        public MainPage MainPage { get; set; }

        public FavouritesPage(MainPage mainPage)
        {
            this.MainPage = mainPage;

            BindingContext = new FavouritesListIncrementalView();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            FavouritesListIncrementalView vm = BindingContext as FavouritesListIncrementalView;

            vm.LoadMoreItemsCommand.Execute(null);
        }

        async void OnElementClick(object sender, ItemTappedEventArgs e)
        {
            IElement element = (IElement)e.Item;

            Page page = null;

            switch ((int)element.TypeElement)
            {
                case (int)ElementType.Book: page = new BookDetails((Book)element, this);
                    break;
                case (int)ElementType.Movie: page = new MovieDetails((Movie)element, this);
                    break;
                case (int)ElementType.Song: page = new SongDetails((Song)element, this);
                    break;
            }
            page.Title = element.MainTitle;

            this.MainPage.SetDetailPage(page);
        }
    }
}