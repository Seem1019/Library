using Library.Core.CustomEntities;
using Library.Core.Entities;
using Library.Core.Exceptions;
using Library.Core.Interfaces;
using Library.Core.QueryFilters;
using Library.Core.Services;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace Library.UnitTest
{
    public class BookServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IOptions<PaginationOptions>> _paginationOptionsMock;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _paginationOptionsMock = new Mock<IOptions<PaginationOptions>>();
            _bookService = new BookService(_unitOfWorkMock.Object, _paginationOptionsMock.Object);
        }

        [Fact]
        public async Task GetBook_WhenCalled_ReturnsBook()
        {
            // Arrange
            int testBookId = 1;
            var testBook = new Book { Isbn = testBookId, Title = "Test Book" };

            _unitOfWorkMock.Setup(u => u.Books.GetByIdAsync(testBookId))
                .ReturnsAsync(testBook);

            // Act
            var result = await _bookService.GetBook(testBookId);

            // Assert
            Xunit.Assert.Equal(testBook, result);
        }

        [Fact]
        public async Task InsertBook_WithExistingPublisher_InsertsBook()
        {
            // Arrange
            var testBook = new Book { Title = "Test Book", PublisherId = 1 };
            var testPublisher = new Publisher { Id = 1, Name = "Test Publisher" };

            _unitOfWorkMock.Setup(u => u.Publishers.GetByIdAsync(testBook.PublisherId))
                .ReturnsAsync(testPublisher);
            _unitOfWorkMock.Setup(u => u.Books.AddAsync(testBook))
                .Returns(Task.CompletedTask); 
            _unitOfWorkMock.Setup(u => u.CommitAsync())
                .ReturnsAsync(1);

            // Act
            await _bookService.InsertBook(testBook);

            // Assert
            _unitOfWorkMock.Verify(u => u.Books.AddAsync(testBook), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }


        [Fact]
        public async Task InsertBook_WithNonExistingPublisher_ThrowsBusinessException()
        {
            // Arrange
            var testBook = new Book { Title = "Test Book", PublisherId = 1 };

            _unitOfWorkMock.Setup(u => u.Publishers.GetByIdAsync(testBook.PublisherId))
                .ReturnsAsync((Publisher)null);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => _bookService.InsertBook(testBook));
        }

        [Fact]
        public async Task DeleteBook_WithExistingBook_DeletesBook()
        {
            // Arrange
            int testBookId = 1;
            var testBook = new Book { Isbn = testBookId, Title = "Test Book" };

            _unitOfWorkMock.Setup(u => u.Books.GetByIdAsync(testBookId))
                .ReturnsAsync(testBook);
            _unitOfWorkMock.Setup(u => u.CommitAsync())
                .ReturnsAsync(1);

            // Act
            await _bookService.DeleteBook(testBookId);

            // Assert
            _unitOfWorkMock.Verify(u => u.Books.Delete(testBook), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }
        [Fact]
        public async Task GetBooks_WithNoFilters_ReturnsAllBooks()
        {
            // Arrange
            var filters = new BookQueryFilter
            {
                PageSize = 10,
                PageNumber = 1
            }; 
            var mockBooks = new List<Book>
            {
                new Book { Isbn = 1, Title = "Book 1" },
                new Book { Isbn = 2, Title = "Book 2" },
                new Book { Isbn = 3, Title = "Book 3" }
            };
            _unitOfWorkMock.Setup(u => u.Books.GetAllWithAuthorsAsync())
                .ReturnsAsync(mockBooks);

            // Act
            var result = await _bookService.GetBooks(filters);

            // Assert
            Assert.Equal(mockBooks.Count, result.TotalCount);
            Assert.Equal(mockBooks.Count, result.Count);

        }
                [Fact]
        public async Task GetBooks_WithTitleFilter_ReturnsFilteredBooks()
        {
            // Arrange
            var filters = new BookQueryFilter 
            {   Title = "Book 2",
                PageSize = 10,
                PageNumber = 1
            };
            var mockBooks = new List<Book>
            {
                new Book { Isbn = 1, Title = "Book 1" },
                new Book { Isbn = 2, Title = "Book 2" },
                new Book { Isbn = 3, Title = "Book 3" }
            };
            _unitOfWorkMock.Setup(u => u.Books.GetAllWithAuthorsAsync())
                .ReturnsAsync(mockBooks);

            // Act
            var result = await _bookService.GetBooks(filters);

            // Assert
            Assert.Single(result);
            Assert.Equal("Book 2", result.FirstOrDefault().Title);
        }
        [Fact]
        public async Task GetBooks_WithIsbnFilter_ReturnsFilteredBooks()
        {
            // Arrange
            var filters = new BookQueryFilter 
            {   Isbn = 2,
                PageSize = 10,
                PageNumber = 1
            };
            var mockBooks = new List<Book>
    {
        new Book { Isbn = 1, Title = "Book 1" },
        new Book { Isbn = 2, Title = "Book 2" },
        new Book { Isbn = 3, Title = "Book 3" }
    };
            _unitOfWorkMock.Setup(u => u.Books.GetAllWithAuthorsAsync())
                .ReturnsAsync(mockBooks);

            // Act
            var result = await _bookService.GetBooks(filters);

            // Assert
            Assert.Single(result);
            Assert.Equal(2, result.FirstOrDefault().Isbn);
        }
        [Fact]
        public async Task GetBooks_WithAuthorIdFilter_ReturnsFilteredBooks()
        {
            // Arrange
            var filter = new BookQueryFilter 
            {   AuthorId = 1,
                PageSize = 10,
                PageNumber = 1
            };
            var books = new List<Book>
    {
        new Book { Isbn = 1, Title = "Book 1", AuthorHasBooks = new List<AuthorHasBook> { new AuthorHasBook { AuthorId = 1 } } },
        new Book { Isbn = 2, Title = "Book 2", AuthorHasBooks = new List<AuthorHasBook> { new AuthorHasBook { AuthorId = 2 } } },
        new Book { Isbn = 3, Title = "Book 3", AuthorHasBooks = new List<AuthorHasBook> { new AuthorHasBook { AuthorId = 1 } } }
    };

            _unitOfWorkMock.Setup(u => u.Books.GetAllWithAuthorsAsync()).ReturnsAsync(books.AsQueryable());

            // Act
            var result = await _bookService.GetBooks(filter);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, book => Assert.Equal(filter.AuthorId, book.AuthorHasBooks.First().AuthorId));
        }

        [Fact]
        public async Task GetBooks_WithPublisherIdFilter_ReturnsFilteredBooks()
        {
            // Arrange
            var filters = new BookQueryFilter 
            {   PublisherId = 1,
                PageSize = 10,
                PageNumber = 1
            };
            var mockBooks = new List<Book>
    {
        new Book { Isbn = 1, Title = "Book 1", PublisherId = 1 },
        new Book { Isbn = 2, Title = "Book 2", PublisherId = 2 },
        new Book { Isbn = 3, Title = "Book 3", PublisherId = 1 }
    };
            _unitOfWorkMock.Setup(u => u.Books.GetAllWithAuthorsAsync())
                .ReturnsAsync(mockBooks);

            // Act
            var result = await _bookService.GetBooks(filters);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, book => Assert.Equal(1, book.PublisherId));
        }

        [Fact]
        public async Task DeleteBook_WithNonExistingBook_ReturnsFalse()
        {
            // Arrange
            int testBookId = 1;

            _unitOfWorkMock.Setup(u => u.Books.GetByIdAsync(testBookId))
                .ReturnsAsync((Book)null);

            // Act
            var result = await _bookService.DeleteBook(testBookId);

            // Assert
            Assert.False(result);
            _unitOfWorkMock.Verify(u => u.Books.Delete(It.IsAny<Book>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
        }
        [Fact]
        public async Task UpdateBook_WithExistingBookAndValidPublisher_UpdatesBook()
        {
            // Arrange
            var testBook = new Book { Isbn = 1, Title = "Test Book", PublisherId = 1 };
            var testPublisher = new Publisher { Id = 1, Name = "Test Publisher" };

            _unitOfWorkMock.Setup(u => u.Books.GetByIdAsync(testBook.Isbn))
                .ReturnsAsync(testBook);
            _unitOfWorkMock.Setup(u => u.CommitAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _bookService.UpdateBook(testBook);

            // Assert
            Assert.True(result);
            _unitOfWorkMock.Verify(u => u.Books.Update(testBook), Times.Once);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateBook_WithNonExistingBook_ReturnsFalse()
        {
            // Arrange
            var testBook = new Book { Isbn = 1, Title = "Test Book", PublisherId = 1 };

            _unitOfWorkMock.Setup(u => u.Books.GetByIdAsync(testBook.Isbn))
                .ReturnsAsync((Book)null);

            // Act
            var result = await _bookService.UpdateBook(testBook);

            // Assert
            Assert.False(result);
            _unitOfWorkMock.Verify(u => u.Books.Update(It.IsAny<Book>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task UpdateBook_WithMismatchedPublisherIds_ThrowsBusinessException()
        {
            // Arrange
            var testBook = new Book { Isbn = 1, Title = "Test Book", PublisherId = 1 };
            var testPublisher = new Publisher { Id = 2, Name = "Test Publisher" };

            _unitOfWorkMock.Setup(u => u.Books.GetByIdAsync(testBook.Isbn))
                .ReturnsAsync(testBook);

            testBook.Publisher = testPublisher;

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => _bookService.UpdateBook(testBook));
        }

    }

}