﻿using IncrementalListView.FormsPlugin;
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
    public partial class MoviesPage : ContentPage, ISupportIncrementalLoading
    {
        public int PageSize { get; set; } = 10;
        public bool HasMoreItems { get; set; }
        public bool IsLoadingIncrementally { get; set; }
        public ICommand LoadMoreItemsCommand { get; set; }

        private readonly IList<Movie> movies = new ObservableCollection<Movie>();
        private int currentPosition = 0;

        public MoviesPage()
        {
            BindingContext = movies;
            LoadMoreItemsCommand = new Command(async () => await LoadMoreItems());

            InitializeComponent();
        }

        private async Task LoadMoreItems()
        {
            IsLoadingIncrementally = true;

            // Add the newly download data to the collection.
            IEnumerable<Movie> moviesCollection = await MoviesManager.GetMoviesManager().GetMovies(currentPosition, this.PageSize);

            foreach (Movie m in moviesCollection)
            {
                movies.Add(m);
            }

            currentPosition += this.PageSize;

            HasMoreItems = await MoviesManager.GetMoviesManager().CountMovies() > currentPosition;

            IsLoadingIncrementally = false;
        }
    }
}