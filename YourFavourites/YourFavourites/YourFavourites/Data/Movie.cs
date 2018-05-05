using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YourFavourites.Data
{
    public class Movie
    {
        // TODO: Rename and use JsonProperty.

        public string user_id { get; set; }
        public string Title { get; set; }
        public string fulltitle { get; set; }
        public string movie_year { get; set; }
        public string Categories { get; set; }
        public string summary { get; set; }
        [JsonProperty("Image URL")]
        public string Image { get; set; }
        public string imdb_id { get; set; }
        public string imdb_rating { get; set; }
        public string runtime { get; set; }
        public string language { get; set; }
        public string ytid { get; set; }
    }
}
