using Newtonsoft.Json;

namespace ProductScraper.Models
{
    internal class Product
    {
        [JsonProperty("productName")]
        private string ProductName { get; set; }
        [JsonProperty("price")]
        private double Price { get; set; }
        [JsonProperty("rating")]
        private double Rating { get; set; }
        //private double Price { get; set; }
        //public string FormattedPrice => $"{this.Price}";
        //private double Rating { get; set; }
        //public string FormattedRating => $"{this.Rating}";

        public Product()
        {

        }
        public Product(string ProductName, double Price, double Rating)
        {
            this.ProductName = ProductName;
            this.Price = Price;
            this.Rating = Rating;
        }
        public override string ToString()
        {
            return "\nProduct name: " + ProductName + "\nPrice: " + Price + "\nRating: " + Rating; 
        }
    }
}
