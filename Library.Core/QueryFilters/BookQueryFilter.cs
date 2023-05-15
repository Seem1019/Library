namespace Library.Core.QueryFilters
{
    public class BookQueryFilter
    {
        /// <summary>
        /// Filters books by title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Filters books by ISBN.
        /// </summary>
        public int? Isbn { get; set; }

        /// <summary>
        /// Filters books by Publisher Id.
        /// </summary>
        public int? PublisherId { get; set; }

        /// <summary>
        /// Filters books by Author Id.
        /// </summary>
        public int? AuthorId { get; set; }

        /// <summary>
        /// Filters books by Publisher Name.
        /// </summary>
        public string PublisherName { get; set; }

        /// <summary>
        /// Filters books by Author Name.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Filters books by number of pages.
        /// Minimum page count to filter by.
        /// </summary>
        public int? MinNumberOfPages { get; set; }

        /// <summary>
        /// Filters books by number of pages.
        /// Maximum page count to filter by.
        /// </summary>
        public int? MaxNumberOfPages { get; set; }
    }

}
