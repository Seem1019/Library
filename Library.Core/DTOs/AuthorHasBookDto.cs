namespace Library.Core.DTOs
{
    public class AuthorHasBookDto
    {
        public int AuthorId { get; set; }
        public int BookIsbn { get; set; }
        public AuthorDto Author { get; set; }
        public BookDto Book { get; set; }
    }
}
