using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.CustomEntities
{
    public class Pagination
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

        public string NextPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }

        public IQueryable<T> ApplyPagination<T>(IQueryable<T> source)
        {
            TotalCount = source.Count();
            source = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize);

            return source;
        }
    }
}
