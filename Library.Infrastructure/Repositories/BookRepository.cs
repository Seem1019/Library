using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.Data;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(TravelLibraryContext context) : base(context)
        {
        }
    }
}
