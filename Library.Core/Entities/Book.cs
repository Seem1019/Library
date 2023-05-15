using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Core.Entities

{
    public partial class Book
    {
        public Book()
        {
            AuthorHasBooks = new HashSet<AuthorHasBook>();
        }

        public int Isbn { get; set; }
        public int PublisherId { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string NumberOfPages { get; set; }


        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<AuthorHasBook> AuthorHasBooks { get; set; }
    }
}
