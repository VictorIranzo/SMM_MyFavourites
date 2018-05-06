using IncrementalListView.FormsPlugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourFavourites.Components;
using YourFavourites.Data;

namespace YourFavourites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SongsPage : ContentPage
    {
        public readonly MainPage mainPage;

        public SongsPage(MainPage mainPage)
        {
            this.mainPage = mainPage;

            BindingContext = new SongsListIncrementalView();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SongsListIncrementalView vm = BindingContext as SongsListIncrementalView;

            vm.LoadMoreItemsCommand.Execute(null);
        }

        async void OnSongClick(object sender, ItemTappedEventArgs e)
        {
            /*
            Page page = new MovieDetails((Movie)e.Item, this);
            page.Title = ((Movie)e.Item).Title;

            this.mainPage.SetDetailPage(page);
            */
        }
    }
}