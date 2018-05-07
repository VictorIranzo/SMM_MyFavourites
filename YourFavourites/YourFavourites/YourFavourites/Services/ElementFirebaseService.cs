using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YourFavourites.Data;

namespace YourFavourites.Services
{
    public class ElementFirebaseService
    {
        private readonly string BASE_URL = "https://smm-yourfavourites.firebaseio.com/";

        public async Task<string> CheckUserExists(string user_id)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            FirebaseDB firebaseDBUsers = firebaseDB.NodePath("users/" + user_id);

            FirebaseResponse getResponse = firebaseDBUsers.Get();

            if (getResponse.Success)
            {
                string response = getResponse.JSONContent;

                if (response.Equals("null")) return "";
                else return response;
            }

            return "";
        }

        public async Task<bool> AddUser(string user_id, string email)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            FirebaseDB firebaseDBUsers = firebaseDB.Node("users");

            string userToAdd = "{\"" + user_id + "\":\"" + email + "\"}";

            FirebaseResponse patchResponse = firebaseDBUsers
                .Patch(userToAdd);

            return patchResponse.Success;
        }

        private string GetRootOfElement(ElementType elementType)
        {
            switch (elementType)
            {
                case ElementType.Book: return "/fav_books/";
                case ElementType.Movie: return "/fav_movies/";
                case ElementType.Song: return "/fav_songs/";
            }

            return "";
        }

        public async Task<bool> AddFavourite(string user_id, IElement element)
        {
            // TODO: Test this method.

            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            string path = user_id + GetRootOfElement(element.TypeElement);

            FirebaseDB firebaseDBUserFavMovies = firebaseDB.NodePath(path);

            IDictionary<string, IElement> IdElementPair = new Dictionary<string, IElement>();
            IdElementPair.Add(element.Id, element);

            string movieToAdd = JsonConvert.SerializeObject(IdElementPair);

            FirebaseResponse patchResponse = firebaseDBUserFavMovies
                .Patch(movieToAdd);

            return patchResponse.Success;
        }

        public async Task<bool> RemoveFavourite(string user_id, IElement element)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            string path = user_id + GetRootOfElement(element.TypeElement) + element.Id;

            FirebaseDB firebaseDBRemoveFavMovies = firebaseDB.NodePath(path);

            FirebaseResponse deleteResponse = firebaseDBRemoveFavMovies
                .Delete();

            return deleteResponse.Success;
        }

        public async Task<bool> CheckIfIsFavourite(string user_id, IElement element)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            string path = user_id + GetRootOfElement(element.TypeElement);

            FirebaseDB firebaseDBUserFavMovies = firebaseDB.NodePath(path);

            FirebaseResponse getResponse = firebaseDBUserFavMovies.Get();

            IDictionary<string, IElement> favMovies = JsonConvert.DeserializeObject<IDictionary<string, IElement>>(getResponse.JSONContent);

            return favMovies == null ? false : favMovies.Keys.Contains(element.Id);
        }
    }
}
