using HtmlAgilityPack;
using ProductScraper;
using System.Runtime.CompilerServices;

public class Program
{
    public static void Main(string[] args)
    {
        Scraper scraper = new Scraper();
        scraper.ParseHtmlDocument();
    }
}