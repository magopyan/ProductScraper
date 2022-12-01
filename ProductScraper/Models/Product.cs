using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
