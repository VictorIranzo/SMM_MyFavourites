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
    public partial class MoviesPage : ContentPage, BackablePage
    {
        public MainPage MainPage { get; set; }

        public MoviesPage(MainPage mainPage)
        {
            this.MainPage = mainPage;

            BindingContext = new MoviesListIncrementalView(MoviesListIncrementalView.TypeLoad.NO_FILTER);

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
            Page page = new MovieDetails((Movie)e.Item, this);
            page.Title = ((Movie)e.Item).MainTitle;

            this.MainPage.SetDetailPage(page);
        }

        void OnFindButtonClicked(Object sender, EventArgs e)
        {
            string filter = entryFilter.Text.ToLower();

            MoviesListIncrementalView moviesListIncrementalView = null;

            if (filter == "") moviesListIncrementalView = new MoviesListIncrementalView(MoviesListIncrementalView.TypeLoad.NO_FILTER);
            else moviesListIncrementalView = new MoviesListIncrementalView(MoviesListIncrementalView.TypeLoad.FILTER_BY_TITLE, filter);

            BindingContext = moviesListIncrementalView;

            moviesListIncrementalView.LoadMoreItemsCommand.Execute(null);
        }
    }
}