using Library.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    /// <summary>
    /// Provides read and write operations for Book entity.
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
        public Task<IEnumerable<Book>> GetAllWithAuthorsAsync();
    }
}
