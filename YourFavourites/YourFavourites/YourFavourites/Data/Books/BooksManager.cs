using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YourFavourites.Data
{
    public class BooksManager
    {
        private const string baseUrl = "https://api.nytimes.com/svc/books/v3/lists.json?api-key=d08a16c185df44058553ae97b8817c93&";

        private IDictionary<string, IEnumerable<Book>> booksByCategory = new Dictionary<string,IEnumerable<Book>>();

        // Possible lists
        private static readonly string TERROR = "terror";

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

            IEnumerable<Book> books = JsonConvert.DeserializeObject<IEnumerable<Book>>(result);

            booksByCategory.Add(category, books);
        }
    }
}
