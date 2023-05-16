using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(TravelLibraryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> GetAllWithAuthorsAsync()
        {
            return await _entities
                .Include(b => b.AuthorHasBooks)
                    .ThenInclude(ahb => ahb.Author)
                .ToListAsync();
        }


    }
}
