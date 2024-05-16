using System.Text.Json.Serialization;

namespace WebApp.Controllers
{
    public class Book
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        [JsonPropertyName("ISBN10")]
        public string ISBN10 { get; set; }

        [JsonPropertyName("ISBN13")]
        public string ISBN13 { get; set; }

        [JsonPropertyName("publishedDate")]
        public string PublishedDate { get; set; }

        [JsonPropertyName("numberOfPages")]
        public int NumberOfPages { get; set; }

        [JsonPropertyName("publisher")]
        public string Publisher { get; set; }

        [JsonPropertyName("reviewScore")]
        public double ReviewScore { get; set; }


        public override string ToString()
        {
            string output = "";

            output += $"Title: {Title}\n";

            output += $"Number of pages: {NumberOfPages}\n";


            return output;
        }

    }
}
