using Newtonsoft.Json;

namespace YourFavourites.Data.Books.Deserializers
{
    public class BookResult
    {
        [JsonProperty("book_details")]
        public Book[] BookDetails { get; set; }
    }
}