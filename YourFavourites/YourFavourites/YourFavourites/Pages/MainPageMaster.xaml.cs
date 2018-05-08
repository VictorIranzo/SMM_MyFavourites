﻿using AndroidAuthorization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YourFavourites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView;

        public MainPageMaster()
        {
            InitializeComponent();

            BindingContext = new MainPageMasterViewModel();
            ListView = MenuItemsListView;
            lblName.Text = AccountManager.GetAccountName();
            imgUser.Source = AccountManager.GetImageURL();
        }

        class MainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainPageMenuItem> MenuItems { get; set; }

            public MainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
                {
                    new MainPageMenuItem { Id = 1, Title = "Favourites" , IconSource = "favourite.png"},
                    new MainPageMenuItem { Id = 2, Title = "Movies" , IconSource = "movie.png"},
                    new MainPageMenuItem { Id = 3, Title = "Songs" , IconSource = "song.png"},
                    new MainPageMenuItem { Id = 4, Title = "Books" , IconSource = "book.png"},
                    new MainPageMenuItem { Id = 5, Title = "Log out" , IconSource = "power.png"}
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}