using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories
{
    internal class AuthorHasBookRepository : BaseRepository<AuthorHasBook>, IAuthorHasBookRepository
    {
        public AuthorHasBookRepository(TravelLibraryContext context) : base(context)
        {
        }
    }
}
