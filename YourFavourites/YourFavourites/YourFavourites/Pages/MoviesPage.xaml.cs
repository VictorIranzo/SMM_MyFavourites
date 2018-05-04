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
using YourFavourites.Data;

namespace YourFavourites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesPage : ContentPage
    {
        public MoviesPage()
        {
            BindingContext = new IncrementalViewModel();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            IncrementalViewModel vm = BindingContext as IncrementalViewModel;

            vm.LoadMoreItemsCommand.Execute(null);
        }
    }
}