using Library.Core.QueryFilters;
using System;

namespace Library.Infrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(BookQueryFilter filter, string actionUrl);
    }
}
