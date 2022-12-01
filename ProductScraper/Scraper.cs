using HtmlAgilityPack;
using Newtonsoft.Json;
using ProductScraper.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductScraper
{
    public class Scraper
    {
        public Scraper()
        {
        }
        public void ScrapeProducts()
        {
            var path = @"../../../Excerpts/products.html";
            var doc = new HtmlDocument();
            try
            {
                doc.Load(path);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Error: Directory " + path + " not found.");
                return;
            }

            if (!CanParseHtmlDocument(doc.ParseErrors))
                return;
            else
            {
                ParseHtmlDocument(doc);
            }
        }

        private void ParseHtmlDocument(HtmlDocument doc)
        {
            if (doc.DocumentNode == null)
            {
                Console.WriteLine("Error: Empty root node.");
                return;
            }
            else
            {
                var divItemNodes = doc.DocumentNode.SelectNodes("//div[@class='item']");
                if (divItemNodes != null && divItemNodes.Count > 0)
                {
                    foreach (HtmlNode divItemNode in divItemNodes)
                    {
                        var ratingNode = divItemNode.Attributes["rating"].Value;
                        double rating;
                        if(ratingNode != null)
                        {
                            rating = double.Parse(ratingNode, CultureInfo.InvariantCulture);

                            // The ratings must be normalized but there is no indication what scales can be used
                            // and how to differentiate between them, so I did the following implementation.
                            // It assumes that if the rating is over 10, then the scale is from 1 to 100,
                            // or if the rating is between 5.01 and and 10, then the scale is from 1 to 10.

                            // If the rating is 40 then it will become 2/5.
                            if (rating > 10)
                                rating = rating / 20;
                            // If the rating is 8 then it will become 4/5.
                            else if (rating > 5)
                                rating = rating / 2;
                        }

                    }
                }

                // Console.WriteLine("\n \n \n" + divItemNode.OuterHtml);
                //var list = new List<Product>();
                //list.Add(new Product("name1", 9.99, 4.1));
                //list.Add(new Product("name2", 1.11, 2.9));
                //JsonConvert.SerializeObject(list, Formatting.Indented);
            }
        }

        private bool CanParseHtmlDocument(IEnumerable<HtmlParseError> errors)
        {
            if (errors != null && errors.Count() > 0)
            {
                ListParseErrors(errors);
                return false;
            }
            else
                return true;
        }
        private void ListParseErrors(IEnumerable<HtmlParseError> errors)
        {
            Console.WriteLine("Error when parsing HTML file on lines:");
            foreach (var htmlParseError in errors)
            {
                Console.WriteLine("- " + htmlParseError.Line);
            }
        }
    }
}
