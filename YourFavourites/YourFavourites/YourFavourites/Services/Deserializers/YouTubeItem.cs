using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YourFavourites.Services.Deserializers
{
    public class YouTubeItem
    {
        [JsonProperty("id")]
        public YouTubeVideoId VideoId { get; set; }
    }
}
