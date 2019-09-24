using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BookStore
{
    public class ItbookStoreSeedApi : IFetchSeedData
    {
        public async Task<IEnumerable<Book>> FetchSeedData(string searchTerm = "mongodb")
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://api.itbook.store/1.0/search/{searchTerm}")
                .ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                dynamic responsebooks = JObject.Parse(content);
                List<Book> books = new List<Book>();
                foreach (dynamic book in responsebooks.books)
                {
                    books.Add(new Book
                    {
                        Title = book.title,
                        SubTitle = book.subtitle,
                        Image = book.image,
                        Isbn = book.isbn13
                    });
                }

                return books.AsEnumerable();
            }

            return await Task.FromResult(Enumerable.Empty<Book>());
        }
    }
}