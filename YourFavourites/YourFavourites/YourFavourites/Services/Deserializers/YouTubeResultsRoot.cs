using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YourFavourites.Services.Deserializers
{
    public class YouTubeResultsRoot
    {
        [JsonProperty("items")]
        public YouTubeItem[] Videos { get; set; }
    }
}
