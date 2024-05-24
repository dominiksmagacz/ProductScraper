using ClosedXML.Excel;

namespace ProductScraper
{
    class ExcelWorker
    {
        public void DataWriter(List<Product> products)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


            using (var workbook = new XLWorkbook())
            {
                var ws = workbook.AddWorksheet("Scrap Produktów");
                ws.Unhide();
                ws.Cell(1, 1).Value = "LP";
                ws.Cell(1, 2).Value = "Nazwa produktu";
                ws.Cell(1, 3).Value = "Cena za produkt";
                ws.Cell(1, 4).Value = "Cena jednostkowa";
                int i = 2;
                foreach (var product in products)
                {
                    
                    ws.Cell(i, 1).Value = (i - 1).ToString();
                    ws.Cell(i, 2).Value = product.ProductName;
                    ws.Cell(i, 3).Value = product.Price;
                    ws.Cell(i, 4).Value = product.BasicPrice;
                    
                    i++;
                }
                workbook.SaveAs(Path.Combine(path, "Scrap.xlsx"));
            }
        }
    }
}