using Library.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    /// <summary>
    /// Provides read and write operations for Book entity.
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
        public IQueryable<Book> GetAllWithAuthors();
    }
}
