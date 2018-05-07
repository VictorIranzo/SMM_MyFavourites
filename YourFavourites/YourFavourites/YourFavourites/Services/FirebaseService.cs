﻿using Newtonsoft.Json;
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

            FirebaseDB firebaseDBUserFavMovies = firebaseDB.NodePath(path);

            FirebaseResponse getResponse = firebaseDBUserFavMovies.Get();

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
