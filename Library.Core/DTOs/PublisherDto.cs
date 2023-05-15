using System.Collections.Generic;

namespace Library.Core.DTOs
{
    public class PublisherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Headquarters { get; set; }
        public ICollection<BookDto> Books { get; set; }
    }

}
