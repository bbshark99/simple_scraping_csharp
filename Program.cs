using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScraperTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GetHtmlAsync();
            Console.ReadKey();
        }

        private static async void GetHtmlAsync()
        {
            var url = "https://miami.craigslist.org/search/reb?min_price=2500&max_price=250000&availabilityMode=0&housing_type=6&sale_date=all+dates&lang=en&cc=gb";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var ProductsHtml = htmlDocument.DocumentNode.Descendants("ul")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("rows")).ToList();

            int i = 0;
            var ProductListItems = ProductsHtml[0].Descendants("li")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("result-row")).ToList();

            foreach (var ProductListItem in ProductListItems)
            {
                Console.WriteLine(ProductListItem.Descendants("a")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Contains("result-title")).FirstOrDefault().InnerText);



            }
            
            

            Console.WriteLine();
        }
    }
}
