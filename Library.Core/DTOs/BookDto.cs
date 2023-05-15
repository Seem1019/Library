namespace Library.Core.DTOs
{
    public class BookDto
    {
        public int Isbn { get; set; }
        public int PublisherId { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string NumberOfPages { get; set; }
    }
}
