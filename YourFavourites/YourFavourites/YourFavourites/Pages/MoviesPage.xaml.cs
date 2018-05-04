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
        public MoviesPage()
        {
            BindingContext = new MoviesListIncrementalView();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MoviesListIncrementalView vm = BindingContext as MoviesListIncrementalView;

            vm.LoadMoreItemsCommand.Execute(null);
        }
    }
}