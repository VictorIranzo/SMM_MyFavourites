using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YourFavourites.Data
{
    public class Book : IElement
    {
        [JsonProperty("primary_isbn13")]
        public string Id {get; set;}

        public int TypeElement { get; set; } = (int)ElementType.Book;

        [JsonProperty("title")]
        public string MainTitle {get; set;}

        [JsonProperty("author")]
        public string SecondTitle {get; set;}

        public string ImageUrl { get; set; } = "https://cdn.pixabay.com/photo/2013/07/12/15/20/author-149694_1280.png";

        [JsonProperty("description")]
        public string Description {get; set;}

        [JsonProperty("contributor")]
        public string FirstFeature {get; set;}

        [JsonProperty("publisher")]
        public string SecondFeature {get; set;}
    }
}
