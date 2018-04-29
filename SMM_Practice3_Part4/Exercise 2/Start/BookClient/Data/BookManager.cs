using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookClient.Data
{
    public class BookManager
    {
        private const string baseUrl = "http://xam150.azurewebsites.net/api/books/";
        private string authorizationKey;

        public async Task<IEnumerable<Book>> GetAll()
        {
            HttpClient httpClient = await GetClient();
            string result = await httpClient.GetStringAsync(baseUrl);

            return JsonConvert.DeserializeObject<IEnumerable<Book>>(result);
        }

        public async Task<Book> Add(string title, string author, string genre)
        {
            Book book = new Book()
            {
                ISBN = string.Empty,
                Authors = new List<string>() { author },
                Title = title,
                Genre = genre,
                PublishDate = DateTime.Now
            };

            HttpClient httpClient = await GetClient();
            string bookSerialized = JsonConvert.SerializeObject(book);
            HttpContent httpContent = new StringContent(bookSerialized,Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(baseUrl, httpContent);
            string bookPosted = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Book>(bookPosted);
        }

        public async Task Update(Book book)
        {
            HttpClient httpClient = await GetClient();

            string bookSerialized = JsonConvert.SerializeObject(book);
            HttpContent httpContent = new StringContent(bookSerialized, Encoding.UTF8, "application/json");

            await httpClient.PutAsync(baseUrl + book.ISBN, httpContent);
        }

        public async Task Delete(string isbn)
        {
            HttpClient httpClient = await GetClient();

            await httpClient.DeleteAsync(baseUrl + isbn);
        }

        private async Task<HttpClient> GetClient()
        {
            HttpClient httpClient = new HttpClient();

            if (string.IsNullOrEmpty(authorizationKey))
            {
                string response = await httpClient.GetStringAsync(baseUrl + "login");
                authorizationKey = JsonConvert.DeserializeObject<string>(response);
            }

            httpClient.DefaultRequestHeaders.Add("Authorization",authorizationKey);
            httpClient.DefaultRequestHeaders.Add("Accept","application/json");

            return httpClient;
        }
    }
}

