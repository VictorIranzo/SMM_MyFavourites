using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YourFavourites.Data
{
    public class SongsManager
    {
        private const string baseUrl = "http://ws.audioscrobbler.com/2.0/?method=chart.gettoptracks&api_key=b3c29d0b7ea4c5202020a19c8a3e00bb&format=json";

        public async Task<IEnumerable<Song>> GetSongs(int offset, int count)
        {
            HttpClient httpClient = new HttpClient();
            string result = await httpClient.GetStringAsync(baseUrl + "&page=" + offset + "&limit=" + count);
            SongsRoot root = JsonConvert.DeserializeObject<SongsRoot>(result);

            return root.tracks.track;
        }
    }
}

