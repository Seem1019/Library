using System;
using System.Collections.Generic;
using System.Linq;

namespace Contacts.Core.CustomEntities
{
    /// <summary>
    /// Represents a subset of a collection of objects that can be individually accessed by index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class PagedList<T> : List<T>
    {
        /// <summary>
        /// Gets or sets the number of the current page.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total number of items.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets a value indicating whether there is a previous page.
        /// </summary>
        public bool HasPreviousPage => CurrentPage > 1;

        /// <summary>
        /// Gets a value indicating whether there is a next page.
        /// </summary>
        public bool HasNextPage => CurrentPage < TotalPages;

        /// <summary>
        /// Gets the number of the next page.
        /// </summary>
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;

        /// <summary>
        /// Gets the number of the previous page.
        /// </summary>
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : (int?)null;

        /// <summary>
        /// Initializes a new instance of the PagedList class that contains elements copied from the specified list
        /// and has sufficient capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="items">The collection whose elements are copied to the new list.</param>
        /// <param name="count">The total number of items in the source.</param>
        /// <param name="pageNumber">The current page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        /// <summary>
        /// Creates a PagedList from a source enumerable.
        /// </summary>
        /// <param name="source">The source enumerable to create the page from.</param>
        /// <param name="pageNumber">The current page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A PagedList of the specified part of the source.</returns>
        public static PagedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}