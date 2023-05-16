using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(TravelLibraryContext context) : base(context)
        {
        }
    }
}
