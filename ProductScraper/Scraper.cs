﻿using HtmlAgilityPack;
using Newtonsoft.Json;
using ProductScraper.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProductScraper
{
    public class Scraper
    {
        public Scraper()
        {
        }
        public void ParseHtmlDocument()
        {
            var htmlFilePath = @"../../../Excerpts/products.html";
            var htmlDoc = new HtmlDocument();
            try
            {
                htmlDoc.Load(htmlFilePath);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Error: Directory " + htmlFilePath + " not found.");
                return;
            }

            if (CanParseHtmlDocument(htmlDoc.ParseErrors))
                ScrapeProducts(htmlDoc);
            else
                return;
        }

        private void ScrapeProducts(HtmlDocument doc)
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
                    List<Product> parsedProductsList = new List<Product>();
                    foreach (HtmlNode divItemNode in divItemNodes)
                    {
                        double rating = ParseRating(divItemNode);
                        double price = ParsePrice(divItemNode);
                        string productName = ParseProductName(divItemNode);
                    }
                }

                // Console.WriteLine("\n \n \n" + divItemNode.OuterHtml);
                //var list = new List<Product>();
                //list.Add(new Product("name1", 9.99, 4.1));
                //list.Add(new Product("name2", 1.11, 2.9));
                //JsonConvert.SerializeObject(list, Formatting.Indented);
            }
        }

        private string ParseProductName(HtmlNode divItemNode)
        {
            var firstImgNode = divItemNode.SelectSingleNode(".//img");
            if(firstImgNode == null)
            {
                Console.WriteLine("No product name found for item node on Line " + divItemNode.Line);
                return "";
            }
            else
            {
                var productNameNode = firstImgNode.Attributes["alt"];
                if (productNameNode == null)
                {
                    Console.WriteLine("No product name found for item node on Line " + divItemNode.Line);
                    return "";
                }
                else
                {
                    string productName = productNameNode.Value;
                    Console.WriteLine(productName);
                    return productName;
                }
            }
        }

        private double ParsePrice(HtmlNode divItemNode)
        {
            var spanDollarsNode = divItemNode.SelectSingleNode(".//span[contains(@class, 'dollars')]");
            var spanCentsNode = divItemNode.SelectSingleNode(".//span[contains(@class, 'cents')]");
            if (spanDollarsNode == null || spanCentsNode == null)
            {
                Console.WriteLine("No product name found for item node on Line " + divItemNode.Line);
                return -1;
            }
            else
            {
                //Regex rgx = new Regex(@"\D");
                //string dollars = rgx.Replace(spanDollarsNode.InnerHtml, "");
                string dollars = spanDollarsNode.InnerHtml.Replace(",", "");             
                string cents = spanCentsNode.InnerHtml;
                Console.WriteLine(dollars + cents, CultureInfo.InvariantCulture);
                return double.Parse(dollars + cents, CultureInfo.InvariantCulture);
            }
        }

        private double ParseRating(HtmlNode divItemNode)
        {
            var ratingNode = divItemNode.Attributes["rating"];
            if (ratingNode == null)
            {
                Console.WriteLine("No rating found for item node on Line " + divItemNode.Line);
                return -1;
            }
            else
            {
                double rating = double.Parse(ratingNode.Value, CultureInfo.InvariantCulture);

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
                Console.WriteLine(rating);
                return rating;
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
