using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YourFavourites.Data;
using YourFavourites.Services;

namespace YourFavourites.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public abstract partial class ElementsPage<TViewModel, TElementDetails, TElement> : BackablePage
        where TViewModel : IncrementalViewModel<TElement>, new()
        where TElementDetails : ElementDetails<TElement>, new()
        where TElement : IElement
	{
        public ElementsPage(MainPage mainPage)
            :base(mainPage)
        {
            BindingContext = new TViewModel();

            SetSpecificIncrementalViews();
        }

        protected abstract void SetSpecificIncrementalViews();

        protected override void OnAppearing()
        {
            base.OnAppearing();

            TViewModel viewModel = BindingContext as TViewModel;

            viewModel.LoadMoreItemsCommand.Execute(null);
        }

        async void OnMovieClick(object sender, ItemTappedEventArgs e)
        {
            TElementDetails detailsPage = new TElementDetails();
            detailsPage.PassElementToElementDetailsView((TElement)e.Item, this);

            detailsPage.Title = ((Movie)e.Item).Title;

            this.MainPage.SetDetailPage(detailsPage);
        }
    }
}