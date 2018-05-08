using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YourFavourites.Services.Deserializers;

namespace YourFavourites.Services
{
    class YouTubeService
    {
        private static readonly string BASE_URL = @"https://www.googleapis.com/youtube/v3/search?part=snippet&key=AIzaSyCWL_ZhiHmzDCg6LmXMi05ELneQfWiDVx8&type=video&q=";
        private static readonly string YouTubeUrl = "https://www.youtube.com/watch?v=";

        public static async Task OpenFirstYouTubeVide(string description)
        {
            HttpClient httpClient = new HttpClient();
            string result = await httpClient.GetStringAsync(BASE_URL + description);

            YouTubeResultsRoot videosResult = JsonConvert.DeserializeObject<YouTubeResultsRoot>(result);

            string video_id = videosResult.Videos.FirstOrDefault().VideoId.VideoId;

            Device.OpenUri(new Uri(YouTubeUrl + video_id));
        }   
    }
}
