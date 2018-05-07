using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace YourFavourites.Services
{
    public abstract class BackablePage : ContentPage
    {
        protected MainPage MainPage { get; set; }

        protected BackablePage(MainPage mainPage)
        {
            this.MainPage = mainPage;
        }

        public async void BackToThisPage()
        {
            this.MainPage.SetDetailPage(this);
        }
    }
}
