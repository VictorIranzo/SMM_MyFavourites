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

namespace YourFavourites
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BookDetails : ContentPage
	{
        readonly BooksPage booksPage;
        readonly Book currentBook;
        bool IsFavourite;

        public BookDetails(Book book, BooksPage booksPage)
        {
            this.currentBook = book;
            this.booksPage = booksPage;

            BindingContext = book;

            FirebaseService firebaseService = new FirebaseService();

            IsFavourite = firebaseService.CheckIfIsFavourite(AccountManager.GetAccountId(), currentBook).Result;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetFavButton();
        }

        void OnAddFavClicked(Object sender, EventArgs e)
        {
            FirebaseService firebaseService = new FirebaseService();

            if (IsFavourite)
            {
                firebaseService.RemoveFavourite(AccountManager.GetAccountId(), currentBook);
                IsFavourite = false;
            }
            else
            {
                firebaseService.AddFavourite(AccountManager.GetAccountId(), currentBook);
                IsFavourite = true;
            }

            SetFavButton();
        }

        private void SetFavButton()
        {
            if (IsFavourite) butAddFavourite.Text = "Remove from favourites";
            else butAddFavourite.Text = "Add to favourites";
        }

        protected override bool OnBackButtonPressed()
        {
            this.booksPage.mainPage.SetDetailPage(this.booksPage);

            return true;
        }
    }
}