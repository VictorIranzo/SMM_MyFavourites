using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YourFavourites.Data
{
    public class Movie : IElement
    {
        [JsonProperty("imdb_id")]
        public string Id { get; set; }

        public int TypeElement { get; set; } = (int) ElementType.Movie;

        [JsonProperty("Title")]
        public string MainTitle { get; set; }

        [JsonProperty("imdb_rating")]
        public string SecondTitle { get; set; }

        [JsonProperty("Image URL")]
        public string ImageUrl { get; set; }

        [JsonProperty("summary")]
        public string Description { get; set; }

        [JsonProperty("Categories")]
        public string FirstFeature { get; set; }

        [JsonProperty("movie_year")]
        public string SecondFeature { get; set; }

        [JsonProperty("ytid")]
        public string YouTubeLink { get; set; }
    }
}
