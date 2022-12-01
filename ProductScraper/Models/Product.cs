using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductScraper.Models
{
    internal class Product
    {
        private string _name;
        private double _price;
        private double _rating;

        public Product(string name, double price, double rating)
        {
            _name = name;
            _price = price;
            _rating = rating;
        }
    }
}
