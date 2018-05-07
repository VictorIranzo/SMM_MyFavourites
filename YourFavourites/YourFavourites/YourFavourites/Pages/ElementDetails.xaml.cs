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

namespace YourFavourites.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public abstract partial class ElementDetails<TElement> : ContentPage
                where TElement : IElement
    {
        private TElement Element { get; set; }
        private BackablePage BottomPage { get; set; }
        private bool IsFavourite { get; set; }

        public void PassElementToElementDetailsView(TElement element, BackablePage bottomPage)
        {
            this.Element = element;
            this.BottomPage = BottomPage;
        }

        protected override bool OnBackButtonPressed()
        {
            this.BottomPage.BackToThisPage();

            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetFavButton();
        }

        protected abstract void SetFavButton();

        void OnAddFavClicked(Object sender, EventArgs e)
        {
            ElementFirebaseService firebaseService = new ElementFirebaseService();

            if (IsFavourite)
            {
                firebaseService.RemoveFavourite(AccountManager.GetAccountId(), this.Element);
                IsFavourite = false;
            }
            else
            {
                firebaseService.AddFavourite(AccountManager.GetAccountId(), this.Element);
                IsFavourite = true;
            }

            SetFavButton();
        }
    }
}