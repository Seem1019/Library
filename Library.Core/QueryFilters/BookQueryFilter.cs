using Library.Core.Entities;
using System;
using System.Linq;

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

        public int PageSize { get; set; } = 5;

        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// filter method
        /// </summary>
        /// <param name="books"></param>
        /// <returns></returns>
        public IQueryable<Book> ApplyFilter(IQueryable<Book> books)
        {
            if (!string.IsNullOrEmpty(Title))
            {
                books = books.Where(x => x.Title.ToLower().Contains(Title.ToLower()));
            }

            if (Isbn.HasValue)
            {
                books = books.Where(x => x.Isbn == Isbn);
            }

            if (PublisherId.HasValue)
            {
                books = books.Where(x => x.PublisherId == PublisherId);
            }

            if (AuthorId.HasValue)
            {
                books = books.Where(x => x.AuthorHasBooks.Any(ahb => ahb.AuthorId == AuthorId));
            }

            if (!string.IsNullOrEmpty(PublisherName))
            {
                books = books.Where(x => x.Publisher.Name.ToLower().Contains(PublisherName.ToLower()));
            }

            if (!string.IsNullOrEmpty(AuthorName))
            {
                books = books.Where(x => x.AuthorHasBooks.Any(ahb => ahb.Author.Name.ToLower().Contains(AuthorName.ToLower())));
            }

            if (MinNumberOfPages.HasValue)
            {
                books = books.Where(x => int.Parse(x.NumberOfPages) >= MinNumberOfPages);
            }

            if (MaxNumberOfPages.HasValue)
            {
                books = books.Where(x => int.Parse(x.NumberOfPages) <= MaxNumberOfPages);
            }

            return books;
        }
    }
}
