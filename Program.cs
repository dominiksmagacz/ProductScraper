
namespace ProductScraper
{
    class ProductScraper
    {
        static void Main(string[] args)
        {
            string url = @"https://www.obi.pl/akcesoria-do-malowania-i-tapetowania/kleje/c/1286";
            string url2 = @"https://www.obi.pl/elektronarzedzia/wiertarki-i-wkretarki/c/3111";

            List<Product> products = new List<Product>();
            _ = new WebsiteWorker(url2, products);
            var excel = new ExcelWorker();
            excel.DataWriter(products);

            System.Console.WriteLine("koniec");

            // excel.DataWriter(products);
        }
    }
}