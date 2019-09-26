using Marten;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Host.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksController(IDocumentStore documentStore)
        {
            DocumentStore = documentStore;
        }

        public IDocumentStore DocumentStore { get; }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            using (IQuerySession session = this.DocumentStore.QuerySession())
            {
               return Ok(await session.Query<Book>().ToListAsync());
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBook(Guid Id)
        {
            using (IQuerySession session = this.DocumentStore.QuerySession())
            {
                return Ok(await session.LoadAsync<Book>(Id));
            }
        }

        [HttpPost()]
        public async Task<IActionResult> CreateBook(Book book)
        {
            using (IDocumentSession session = this.DocumentStore.LightweightSession())
            {
                session.Insert(book);
                await session.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBook), new { Id = book.Id });
            }
        }
    }
}
