using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YourFavourites.Data.Books.Deserializers;

namespace YourFavourites.Data
{
    public class BooksManager
    {
        private const string baseUrl = "https://api.nytimes.com/svc/books/v3/lists.json?api-key=d08a16c185df44058553ae97b8817c93&";

        private IDictionary<string, IEnumerable<Book>> booksByCategory = new Dictionary<string,IEnumerable<Book>>();

        private BooksManager(){}

        private static BooksManager instance;
        public static BooksManager GetBooksManager() {
            if (instance == null) instance = new BooksManager();
            return instance;
        }

        public static IDictionary<string,string> Categories = new Dictionary<string,string>()
        {
            { "Fiction", "combined-print-and-e-book-fiction" },
            { "Nonfiction", "combined-print-and-e-book-nonfiction" },
            { "Crime and Punishment", "crime-and-punishment" },
            { "Culture", "culture" },
            { "Young Adult", "young-adult" },
            { "Science", "science" },
            { "Espionage", "espionage" },
            { "Business Books", "business-books" },
            { "Travel", "travel" },
            { "Celebrities", "celebrities" },
            { "Animals", "animals" },
            { "Humor", "humor" },
            { "Food and Fitness", "food-and-fitness" },
            { "Manga", "manga" }
        };

        public static string GetListNameByCategory(string selectedCategory)
        {
            Categories.TryGetValue(selectedCategory, out string list);

            return list;
        }

        public async Task<IEnumerable<Book>> GetBooks(int offset, int count, string category)
        {
            if (!booksByCategory.ContainsKey(category))
            {
                await GetBooksCategory(category);
            }
            
            booksByCategory.TryGetValue(category, out IEnumerable<Book> books);

            return books?.Skip(offset).Take(count);
        }

        private async Task GetBooksCategory(string category)
        {
            HttpClient httpClient = new HttpClient();
            string result = await httpClient.GetStringAsync(baseUrl + "&list=" + category);

            BooksRoot bookRoot = JsonConvert.DeserializeObject<BooksRoot>(result);

            List<Book> books = new List<Book>();

            foreach (BookResult br in bookRoot.Results)
            {
                if (br.BookDetails[0] != null)
                {
                    books.Add(br.BookDetails[0]);
                }
            }

            booksByCategory.Add(category, books);
        }
    }
}
