using System.Collections.Generic;

#nullable disable

namespace Library.Core.Entities 

{
    public partial class Publisher : BaseEntity
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public string Name { get; set; }
        public string Headquarters { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
