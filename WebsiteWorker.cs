using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace ProductScraper
{
    class WebsiteWorker
    {
        public WebsiteWorker(string url, List<Product> products)
        {

            var driver = new ChromeDriver();
            driver.Url = url;
            string quantityProductsInformation = driver.FindElement(By.XPath("//div[@class='variants hidden-phone hidden-tablet']")).Text;     //pierwszy wiersz z informacją o ilości produktów
            string pages = driver.FindElement(By.XPath("//a[@class='pagination-bar__link-refs js-pagination-link-refs']")).Text;    //liczba stron w paginacji
            // var goNextPage = driver.FindElement(By.XPath("//span[@class='arrow-right']"));
            int index = pages.IndexOf("z");
            pages = pages.Substring(index + 1);
            _ = int.TryParse(pages, out var quantityPages);
            _ = int.TryParse(quantityProductsInformation.Substring(11, 4), out int quantityProducts);
            int count = 0;

            for (int i = 0; i < quantityPages; i++)
            {
                Thread.Sleep(2000);
                var quantityOfElements = driver.FindElements(By.XPath("//span[@class='description']")).Count;
                for (int j = 0; j < quantityOfElements; j++)
                {
                    var product = new Product();
                    if (count == quantityProducts)
                        break;
                    count++;

                    product.ProductName = driver.FindElement(By.XPath($"(//span[@class='description'])[{j + 1}]"), 10).Text;
                    product.Price = driver.FindElement(By.XPath($"(//span[@class='price'])[{j + 1}]"), 10).Text;
                    product.BasicPrice = driver.FindElement(By.XPath($"(//span[@class='price-basic'])[{j + 1}]"), 10).GetAttribute("data-csscontent");
                    products.Add(product);
                }
                try
                {
                    driver.FindElement(By.XPath("//button[@class='disc-cp-modal__button-primary js-disc-cp-deny-all']")).Click();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("brak przycisku cookie");
                }


                try
                {
                    driver.FindElement(By.XPath("//span[@class='arrow-right']")).Click();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Przyciska w przełączenia na następną stronę jest nieaktywny");
                }
            }
            driver.Quit();
        }
    }
}