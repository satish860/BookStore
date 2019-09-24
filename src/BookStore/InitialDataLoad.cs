// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitialDataLoad.cs" company="VMware, Inc.">
//   Copyright © 2018 VMware, Inc. All rights reserved.
//   This product is protected by copyright and intellectual property laws in the United States and other countries as well as by international treaties.
//   VMware products may be covered by one or more patents listed at http://www.vmware.com/go/patents
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Marten;
using Marten.Schema;

namespace BookStore
{
    public class InitialDataLoad : IInitialData
    {
        private readonly Book[] _bookstoreData;

        public InitialDataLoad(Book[] bookstoreData)
        {
            _bookstoreData = bookstoreData;
        }
        
        public void Populate(IDocumentStore store)
        {
            using (var session = store.LightweightSession())
            {
                session.Store(_bookstoreData);
                session.SaveChanges();
            }
        }
    }
}