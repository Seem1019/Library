using Contacts.Core.CustomEntities;
using Library.Core.CustomEntities;
using Library.Core.Entities;
using Library.Core.Exceptions;
using Library.Core.Interfaces;
using Library.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public BookService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        public async Task<Book> GetBook(int id)
        {
            return await _unitOfWork.Books.GetByIdAsync(id);
        }

        public async Task <PagedList<Book>> GetBooks(BookQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var books = await _unitOfWork.Books.GetAllAsync();

            if (!string.IsNullOrEmpty(filters.Title))
            {
                books = books.Where(x => x.Title.ToLower().Contains(filters.Title.ToLower()));
            }

            if (filters.Isbn.HasValue)
            {
                books = books.Where(x => x.Isbn == filters.Isbn);
            }

            if (filters.PublisherId.HasValue)
            {
                books = books.Where(x => x.PublisherId == filters.PublisherId);
            }

            if (filters.AuthorId.HasValue)
            {
                books = books.Where(x => x.AuthorHasBooks.Any(ahb => ahb.AuthorId == filters.AuthorId));
            }

            if (!string.IsNullOrEmpty(filters.PublisherName))
            {
                books = books.Where(x => x.Publisher.Name.ToLower().Contains(filters.PublisherName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filters.AuthorName))
            {
                books = books.Where(x => x.AuthorHasBooks.Any(ahb => ahb.Author.Name.ToLower().Contains(filters.AuthorName.ToLower())));
            }

            if (filters.MinNumberOfPages.HasValue)
            {
                books = books.Where(x => int.Parse(x.NumberOfPages) >= filters.MinNumberOfPages);
            }

            if (filters.MaxNumberOfPages.HasValue)
            {
                books = books.Where(x => int.Parse(x.NumberOfPages) <= filters.MaxNumberOfPages);
            }

            var pagedBooks = PagedList<Book>.Create(books, filters.PageNumber, filters.PageSize);
            return pagedBooks;
        }

        public async Task InsertBook(Book book)
        {
            var publisher = await _unitOfWork.Publishers.GetByIdAsync(book.PublisherId);
            if (publisher == null)
            {
                throw new BusinessException("Publisher doesn't exist");
            }

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> UpdateBook(Book book)
        {
            var existingBook = await _unitOfWork.Books.GetByIdAsync(book.Isbn);

            if (existingBook == null)
            {
                return false;
            }

            // Check that the PublisherId matches the Publisher's Id
            if (book.Publisher != null && book.PublisherId != book.Publisher.Id)
            {
                throw new BusinessException("The PublisherId of the book does not match the Id of the Publisher entity");
            }

            existingBook.PublisherId = book.PublisherId;
            existingBook.NumberOfPages = book.NumberOfPages;
            existingBook.Synopsis = book.Synopsis;
            existingBook.Title = book.Title;

            _unitOfWork.Books.Update(existingBook);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            _unitOfWork.Books.Delete(book);
            await _unitOfWork.CommitAsync();
            return true;
        }

  
    }
}
