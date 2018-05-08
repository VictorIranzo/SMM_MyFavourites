using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using YourFavourites.Data;

namespace YourFavourites.Services
{
    public class FirebaseService
    {
        private readonly string BASE_URL = "https://smm-yourfavourites.firebaseio.com/";

        public async Task<bool> CheckUserExists(User user)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            FirebaseDB firebaseDBUsers = firebaseDB.NodePath("users/" + user.Email);

            FirebaseResponse getResponse = firebaseDBUsers.Get();

            if (getResponse.Success)
            {
                string response = getResponse.JSONContent;

                if (response.Equals("null")) return false;
                else return true;
            }

            return false;
        }

        public async Task<bool> AddUser(User user)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            FirebaseDB firebaseDBUsers = firebaseDB.Node("users");

            IDictionary<string, User> IdElementPair = new Dictionary<string, User>();
            IdElementPair.Add(user.Email, user);

            string userToAdd = JsonConvert.SerializeObject(IdElementPair);

            FirebaseResponse patchResponse = firebaseDBUsers
                .Patch(userToAdd);

            return patchResponse.Success;
        }

        private string GetRootOfElement(int elementType)
        {
            switch (elementType)
            {
                case (int)ElementType.Book: return "/fav_books/";
                case (int)ElementType.Movie: return "/fav_movies/";
                case (int)ElementType.Song: return "/fav_songs/";
            }

            return "";
        }

        public async Task<bool> AddFavourite(string user_id, IElement element)
        {
            // TODO: Test this method.

            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            string path = user_id + GetRootOfElement(element.TypeElement);

            FirebaseDB firebaseDBUserFavItems = firebaseDB.NodePath(path);

            if (element is Song)
            {
                ((Song)element).image = null;
                ((Song)element).artist = null;
            }

            IDictionary<string, IElement> IdElementPair = new Dictionary<string, IElement>();
            IdElementPair.Add(element.Id, element);

            string favToAdd = JsonConvert.SerializeObject(IdElementPair);

            FirebaseResponse patchResponse = firebaseDBUserFavItems
                .Patch(favToAdd);

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

            FirebaseDB firebaseDBUserFavorites = firebaseDB.NodePath(path);

            FirebaseResponse getResponse = firebaseDBUserFavorites.Get();

            if (getResponse.JSONContent.Equals("null")) return false;

            IEnumerable<string> elementKeys = null;
            switch (element.TypeElement)
            {
                case (int)ElementType.Book: elementKeys = DeserializeBooks(getResponse.JSONContent).Keys;
                    break;
                case (int)ElementType.Movie: elementKeys = DeserializeMovies(getResponse.JSONContent).Keys;
                    break;
                case (int)ElementType.Song: elementKeys = DeserializeSongs(getResponse.JSONContent).Keys;
                    break;
            }

            return elementKeys == null ? false : elementKeys.Contains(element.Id);            
        }

        public IEnumerable<IElement> GetUserFavorites(int offset, int count, string user_id)
        {
            List<IElement> result = new List<IElement>();

            result.AddRange(GetFavouriteBooks(user_id));
            result.AddRange(GetFavouriteMovies(user_id));
            result.AddRange(GetFavouriteSongs(user_id));

            return result.OrderBy(e => e.MainTitle).Skip(offset).Take(count);
        }

        public IEnumerable<Book> GetFavouriteBooks(string user_id)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);
            string pathFavoriteBooks = user_id + GetRootOfElement((int)ElementType.Book);

            FirebaseDB firebaseDBUserFavorites = firebaseDB.NodePath(pathFavoriteBooks);
            FirebaseResponse getResponse = firebaseDBUserFavorites.Get();

            if (getResponse.JSONContent.Equals("null")) return new List<Book>();

            return DeserializeBooks(getResponse.JSONContent).Values;
        }

        public IEnumerable<Movie> GetFavouriteMovies(string user_id)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);
            string pathFavoriteMovies = user_id + GetRootOfElement((int)ElementType.Movie);

            FirebaseDB firebaseDBUserFavorites = firebaseDB.NodePath(pathFavoriteMovies);
            FirebaseResponse getResponse = firebaseDBUserFavorites.Get();

            if (getResponse.JSONContent.Equals("null")) return new List<Movie>();

            return DeserializeMovies(getResponse.JSONContent).Values;
        }

        public IEnumerable<Song> GetFavouriteSongs(string user_id)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);
            string pathFavoriteSongs = user_id + GetRootOfElement((int)ElementType.Song);

            FirebaseDB firebaseDBUserFavorites = firebaseDB.NodePath(pathFavoriteSongs);
            FirebaseResponse getResponse = firebaseDBUserFavorites.Get();

            if (getResponse.JSONContent.Equals("null")) return new List<Song>();

            return DeserializeSongs(getResponse.JSONContent).Values;
        }

        private Dictionary<string, Book> DeserializeBooks(string jSONContent)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, Book>>(jSONContent);
        }

        private Dictionary<string, Movie> DeserializeMovies(string jSONContent)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, Movie>>(jSONContent);
        }

        private Dictionary<string, Song> DeserializeSongs(string jSONContent)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, Song>>(jSONContent);
        }
    }
}
