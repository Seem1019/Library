using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Core.Entities
{
    public partial class Author
    {
        public Author()
        {
            AuthorHasBooks = new HashSet<AuthorHasBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<AuthorHasBook> AuthorHasBooks { get; set; }
    }
}
