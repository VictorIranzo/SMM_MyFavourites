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
    public partial class MoviesPage : ContentPage
    {
        private readonly MainPage mainPage;

        public MoviesPage(MainPage mainPage)
        {
            this.mainPage = mainPage;

            BindingContext = new MoviesListIncrementalView();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MoviesListIncrementalView vm = BindingContext as MoviesListIncrementalView;

            vm.LoadMoreItemsCommand.Execute(null);
        }

        async void OnMovieClick(object sender, ItemTappedEventArgs e)
        {
            Page page = new MovieDetails((Movie)e.Item);
            page.Title = ((Movie)e.Item).Title;

            this.mainPage.SetDetailPage(page);
        }
    }
}