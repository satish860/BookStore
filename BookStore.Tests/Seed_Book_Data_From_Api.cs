// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Seed_Book_Data_From_Api.cs" company="VMware, Inc.">
//   Copyright © 2018 VMware, Inc. All rights reserved.
//   This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
//   VMware products may be covered by one or more patents listed at http://www.vmware.com/go/patents
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Marten;
using Xunit;

namespace BookStore.Tests
{
    public class SeedBookDataFromApi
    {
        [Fact]
        public async Task Create_Initial_Data_From_IT_Store_API()
        {
            IFetchSeedData seedData = new ItbookStoreSeedApi();
            var books = await seedData.FetchSeedData();
            books.Count().Should().Be(10);
        }
        
        [Fact]
        public async Task Verify_If_the_Data_Is_available_After_the_Store_Creation()
        {
            var pDocumentStore = DocumentStore.For(_ =>
            {
                _.Connection("host=localhost;database=BookStore;password=password;username=postgres");
            });
            pDocumentStore.Advanced.Clean.DeleteAllDocuments();
            IFetchSeedData seedData = new ItbookStoreSeedApi();
            var books = await seedData.FetchSeedData();
            var store = DocumentStore.For(_ =>
            {
                _.Connection("host=localhost;database=BookStore;password=password;username=postgres");
                _.InitialData.Add(new InitialDataLoad(books.ToArray()));
            });

            using (var query = store.QuerySession())
            {
                var count = await query.Query<Book>().CountAsync();
                count.Should().Be(10);
            }
        }
    }
}