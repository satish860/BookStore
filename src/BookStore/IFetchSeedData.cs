using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore
{
    public interface IFetchSeedData
    {
        Task<IEnumerable<Book>> FetchSeedData(string searchTerm = "mongodb");
    }
}