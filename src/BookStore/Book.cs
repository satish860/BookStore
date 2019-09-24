using System;

namespace BookStore
{
    public class Book
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Isbn { get; set; }

        public string Price { get; set; }

        public string Image { get; set; }

        public string Url { get; set; }
    }
}
