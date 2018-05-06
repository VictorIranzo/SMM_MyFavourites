using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace YourFavourites.Services
{
    public class FirebaseService
    {
        private readonly string BASE_URL = "https://smm-yourfavourites.firebaseio.com/";
        private readonly string JSON = ".json";

        public async Task<string> CheckUserExists(string id)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add
                                        (new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(BASE_URL + "users/" + id + JSON);

            return "";
        }

        public async Task<bool> AddUser(string id, string email)
        {
            HttpClient httpClient = new HttpClient();

            using (HttpRequestMessage RequestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), BASE_URL + "users" +JSON))
            {
                RequestMessage.Content = new StringContent("{" + id + ":" + email + "}", Encoding.UTF8, "application/json");
                using (HttpResponseMessage ResponseMessage = await httpClient.SendAsync(RequestMessage))
                {
                    string result = await ResponseMessage.Content.ReadAsStringAsync();

                    if (ResponseMessage.StatusCode == HttpStatusCode.NoContent)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
