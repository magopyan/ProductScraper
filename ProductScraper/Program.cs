using ProductScraper;

public class Program
{
    public static void Main(string[] args)
    {
        Scraper scraper = new Scraper();
        scraper.ParseHtmlDocument();
    }
}