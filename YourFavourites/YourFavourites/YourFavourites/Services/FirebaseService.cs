using Newtonsoft.Json;
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

        public async Task<string> CheckUserExists(string id)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            FirebaseDB firebaseDBUsers = firebaseDB.NodePath("users/" + id);

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

        public async Task<bool> AddFavouriteMovie(string user_id, Movie movie)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            string path = user_id + "/fav_movies/"; 

            FirebaseDB firebaseDBUserFavMovies = firebaseDB.NodePath(path);

            string movieToAdd = "{\"" + movie.imdb_id + "\":\"" + movie.Title + "\"}";

            FirebaseResponse patchResponse = firebaseDBUserFavMovies
                .Patch(movieToAdd);

            return patchResponse.Success;
        }

        public async Task<bool> RemoveFavouriteMovie(string user_id, Movie movie)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            string path = user_id + "/fav_movies/" + movie.imdb_id;

            FirebaseDB firebaseDBRemoveFavMovies = firebaseDB.NodePath(path);

            FirebaseResponse deleteResponse = firebaseDBRemoveFavMovies
                .Delete();

            return deleteResponse.Success;
        }

        public async Task<bool> CheckIfIsFavourite(string user_id, Movie movie)
        {
            FirebaseDB firebaseDB = new FirebaseDB(BASE_URL);

            string path = user_id + "/fav_movies/";

            FirebaseDB firebaseDBUserFavMovies = firebaseDB.NodePath(path);

            FirebaseResponse getResponse = firebaseDBUserFavMovies.Get();

            IDictionary<string, string>  favMovies = JsonConvert.DeserializeObject<IDictionary<string, string>>(getResponse.JSONContent);

            return favMovies == null ? false : favMovies.Keys.Contains(movie.imdb_id);
        }
    }
}
