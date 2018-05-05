using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YourFavourites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        public void SetDetailPage(Page page)
        {
            Detail = new NavigationPage(page);
            IsPresented = false;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MainPageMenuItem;
            if (item == null)
                return;

            Page page = null;

            switch (item.Title)
            {
                case "Movies":
                    page = new MoviesPage(this);
                    break;
                case "Profile":
                    page = new ProfilePage();
                    break;
            }

            page.Title = item.Title;

            SetDetailPage(page);

            MasterPage.ListView.SelectedItem = null;
        }
    }
}