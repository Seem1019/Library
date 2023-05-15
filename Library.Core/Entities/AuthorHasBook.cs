using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Core.Entities

{
    public partial class AuthorHasBook
    {
        public int AuthorId { get; set; }
        public int BookIsbn { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
