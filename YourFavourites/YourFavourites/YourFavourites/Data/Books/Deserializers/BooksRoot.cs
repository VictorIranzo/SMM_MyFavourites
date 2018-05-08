using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YourFavourites.Data.Books.Deserializers
{
    public partial class BooksRoot
    {
        [JsonProperty("results")]
        public BookResult[] Results { get; set; }
    }
}
