using HtmlAgilityPack;
using Newtonsoft.Json;
using ProductScraper.Models;
using System;
using System.Collections.Generic;
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
                var list = new List<Product>();
                list.Add(new Product("name1", 9.99, 4.1));
                list.Add(new Product("name2", 1.11, 2.9));
                list.ForEach(prod => Console.WriteLine(JsonConvert.SerializeObject(prod, Formatting.Indented)));
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
