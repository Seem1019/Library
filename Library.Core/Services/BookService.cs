using Contacts.Core.CustomEntities;
using Library.Core.CustomEntities;
using Library.Core.Entities;
using Library.Core.Exceptions;
using Library.Core.Interfaces;
using Library.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Runtime.CompilerServices;
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


        public async Task<ResponseModel<Book>> GetBooks(BookQueryFilter filters = null)
        {
            var response = new ResponseModel<Book>();
            var books = _unitOfWork.Books.GetAllWithAuthors();

            if (filters != null)
            {
                books = filters.ApplyFilter(books);
                books = response.ApplyPagination(books);
            }

            response.Data = books.ToList();
            return response;
        }

        public async Task<ResponseModel<Book>> InsertBook(Book book)
        {
            var response = new ResponseModel<Book>();

            var publisher = await _unitOfWork.Publishers.GetByIdAsync(book.PublisherId);
            if (publisher == null)
            {
                response.IsError = true;
                response.Message = "Publisher doesn't exist";
            }

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CommitAsync();

            return response;
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

       

            _unitOfWork.Books.Update(book);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> DeleteBook(int id)
        {

            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
            {
                return false;
            }
            _unitOfWork.Books.Delete(book);
            await _unitOfWork.CommitAsync();
            return true;
        }

       


    }
}
