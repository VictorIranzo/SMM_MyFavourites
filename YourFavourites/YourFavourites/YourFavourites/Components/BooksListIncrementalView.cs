using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YourFavourites.Data;

namespace YourFavourites.Components
{
    public class BooksIncrementalView : IncrementalViewModel<Book>
    {
        private string category;

        private BooksIncrementalView() { }

        public BooksIncrementalView(string category)
            :base()
        {
            this.category = category;
        }

        protected async override Task AddItems()
        {
            IEnumerable<Book> booksCollection = null;
            booksCollection = await BooksManager.GetBooksManager().GetBooks(this.CurrentPosition, this.PageSize, this.category);

            foreach (Book book in booksCollection)
            {
                this.MyItems.Add(book);
            }
        }

        protected async override Task<bool> CheckMoreItems()
        {
            return 500 > this.CurrentPosition;
        }
    }
}

