using IncrementalListView.FormsPlugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace YourFavourites
{
    public abstract class IncrementalViewModel<T> : INotifyPropertyChanged, ISupportIncrementalLoading
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<T> MyItems { get; set; }

        #region ISupportIncrementalLoading Implementation

        public int PageSize { get; set; } = 20;

        public int CurrentPosition { get; set; }

        public ICommand LoadMoreItemsCommand { get; set; }

        bool isLoadingIncrementally;
        public bool IsLoadingIncrementally
        {
            get { return isLoadingIncrementally; }
            set
            {
                isLoadingIncrementally = value;
                OnPropertyChanged("IsLoadingIncrementally");
            }
        }

        bool hasMoreItems;
        public bool HasMoreItems
        {
            get { return hasMoreItems; }
            set
            {
                hasMoreItems = value;
                OnPropertyChanged("HasMoreItems");
            }
        }

        #endregion

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public IncrementalViewModel()
        {
            MyItems = new ObservableCollection<T>();

            LoadMoreItemsCommand = new Command(async () => await LoadMoreItems());

            HasMoreItems = true;

            CurrentPosition = 0;
        }

        async Task LoadMoreItems()
        {
            IsLoadingIncrementally = true;

            await AddItems();

            HasMoreItems = await CheckMoreItems();

            CurrentPosition += PageSize;

            IsLoadingIncrementally = false;
        }

        protected abstract Task AddItems();

        protected abstract Task<bool> CheckMoreItems();

    }
}
