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
            // Instanciating with base URL  
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            // Referring to Node with name "Teams"  
            FirebaseDB firebaseDBTeams = firebaseDB.NodePath("users/" + id);
            FirebaseResponse getResponse = firebaseDBTeams.Get();

            if (getResponse.Success)
            {
                string s = getResponse.JSONContent;

                if (s.Equals("null")) return "";
                else return s;
            }

            return "";
        }

        public async Task<bool> AddUser(string id, string email)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            // Referring to Node with name "Teams"  
            FirebaseDB firebaseDBTeams = firebaseDB.Node("users");

            string s = "{\"" + id + "\":\"" + email + "\"}";

            FirebaseResponse patchResponse = firebaseDBTeams
                .Patch(s);

            HttpClient httpClient = new HttpClient();

            return true;
        }
    }
}
