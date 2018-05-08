using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace YourFavourites.Data
{
    public class Song : IElement
    {
        [JsonProperty("mbid")]
        public string Id {get; set;}

        public int TypeElement {get; set;}

        [JsonProperty("name")]
        public string MainTitle {get; set;}

        public string SecondTitle {get; set;}

        public string ImageUrl {get; set;}

        [JsonProperty("listeners")]
        public string Description {get; set;}

        [JsonProperty("playcount")]
        public string FirstFeature {get; set;}

        [JsonProperty("duration")]
        public string SecondFeature {get; set;}

        public SongArtist artist { get; set; }
        public List<SongImage> image { get; set; }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (image != null)
            {
                ImageUrl = image.Where(i => i.size.Equals("large")).Select(i => i.image).FirstOrDefault();
            }
            if (artist != null)
            {
                SecondTitle = artist.name;
            }
        }
    }
}
