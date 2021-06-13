using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.ConsoleApp
{
    public class AdvancedParser
    {
        public async Task Parse()
        {
            var sites = new List<string> { "http://site1.ru", "http://site2.ru", "http://site3.ru" };

            var items = ParseItem(GetCategoryItems(GetMenuItems(sites)));
            await foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        public async IAsyncEnumerable<string> GetMenuItems(IEnumerable<string> sites)
        {
            foreach (var site in sites)
            {
                Console.WriteLine($"Menu item parser: {site}");
                for (int i = 0; i < 4; i++)
                {
                    await Task.Delay(20);
                    yield return $"{site}/{i}";

                }
            }
        }
        public async IAsyncEnumerable<string> GetCategoryItems(IAsyncEnumerable<string> siteMenuItems)
        {
            await foreach (var siteMenuItem in siteMenuItems)
            {
                Console.WriteLine($"Category items parser: {siteMenuItem}");
                for (int i = 0; i < 4; i++)
                {
                    yield return $"{siteMenuItem}/{i}";
                }
            }
        }
        public async IAsyncEnumerable<ParseResult> ParseItem(IAsyncEnumerable<string> itemsUrls)
        {
            await foreach (var itemUrl in itemsUrls)
            {
                Console.WriteLine($"Items parser: {itemUrl}");
                yield return new ParseResult { Url = itemUrl };
            }
        }
    }
}
