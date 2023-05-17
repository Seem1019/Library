using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(TravelLibraryContext context) : base(context)
        {
        }

        public IQueryable<Book> GetAllWithAuthors()
        {
            return _entities
                .Include(b => b.AuthorHasBooks)
                    .ThenInclude(ahb => ahb.Author);
        }


    }
}
