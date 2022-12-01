using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                Console.WriteLine("Success");
                //if (htmlDoc.DocumentNode != null)
                //{
                //    HtmlAgilityPack.HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

                //    if (bodyNode != null)
                //    {
                //        // Do something with bodyNode
                //    }
                //}
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
