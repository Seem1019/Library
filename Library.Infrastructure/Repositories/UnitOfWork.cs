using Library.Core.Interfaces;
using Library.Infrastructure.Data;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TravelLibraryContext _context;
        private IAuthorRepository _authorRepository;
        private IBookRepository _bookRepository;
        private IPublisherRepository _publisherRepository;
        private IAuthorHasBookRepository _authorHasBookRepository;
        private ISecurityRepository _securityRepository;

        public UnitOfWork(TravelLibraryContext context)
        {
            _context = context;
        }
        public IAuthorRepository Authors => _authorRepository ??= new AuthorRepository(_context);
        public IBookRepository Books => _bookRepository ??= new BookRepository(_context);
        public IPublisherRepository Publishers => _publisherRepository ??= new PublisherRepository(_context);
        public IAuthorHasBookRepository AuthorHasBooks => _authorHasBookRepository ??= new AuthorHasBookRepository(_context);
        public ISecurityRepository SecurityRepository => _securityRepository ??= new SecurityRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
