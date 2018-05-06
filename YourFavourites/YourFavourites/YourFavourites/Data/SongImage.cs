using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YourFavourites.Data
{
    public class SongImage
    {
        [JsonProperty("#text")]
        public string image { get; set; }
        public string size { get; set; }
    }
}
