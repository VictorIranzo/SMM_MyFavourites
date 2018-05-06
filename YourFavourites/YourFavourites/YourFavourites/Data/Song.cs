using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YourFavourites.Data
{
    public class Song
    {
        public string name { get; set; }
        public string duration { get; set; }
        public string playcount { get; set; }
        public string listeners { get; set; }
        public string mbid { get; set; }
        public string url { get; set; }
        public SongArtist artist { get; set; }
        public List<SongImage> image { get; set; }

        public string ArtistName { get { return artist.name; } }

        public string Image { get { return image.Where(i => i.size.Equals("large")).Select(i => i.image).FirstOrDefault(); } }
    }
}
