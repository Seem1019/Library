using AutoMapper;
using Library.Api.Responses;
using Library.Core.CustomEntities;
using Library.Core.DTOs;
using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Core.QueryFilters;
using Library.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        private readonly IUriService _uriService;

        public BooksController(IMapper mapper, IBookService bookService, IUriService uriService)
        {
            _mapper = mapper;
            _bookService = bookService;
            _uriService = uriService;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] BookQueryFilter filters)
        {
            var books = await _bookService.GetBooks(filters);
            var booksDto = _mapper.Map<List<BookDto>>(books);
            var metadata = new Metadata
            {
                TotalCount = books.TotalCount,
                PageSize = books.PageSize,
                CurrentPage = books.CurrentPage,
                TotalPages = books.TotalPages,
                HasNextPage = books.HasNextPage,
                HasPreviousPage = books.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetBooks))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetBooks))).ToString()
            };

            var response = new ApiResponse<IEnumerable<BookDto>>(booksDto);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(response);
        }
        // GET: api/Books/{isbn}
        [HttpGet("{isbn}", Name = nameof(GetBook))]
        public async Task<IActionResult> GetBook(int isbn)
        {
            var book = await _bookService.GetBook(isbn);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(new ApiResponse<BookDto>(_mapper.Map<BookDto>(book)));
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            var result = await _bookService.InsertBook(book);

            bookDto = _mapper.Map<BookDto>(book);

            return result.IsError ? BadRequest(result) : CreatedAtRoute(nameof(GetBook), new { isbn = bookDto.Isbn }, new ApiResponse<BookDto>(bookDto));
        }

        // PUT: api/Books/{isbn}
        [HttpPut("{isbn}")]
        public async Task<IActionResult> UpdateBook(int Isbn, BookDto bookDto)
        {
            if (Isbn != bookDto.Isbn)
            {
                return BadRequest();
            }

            var book = _mapper.Map<Book>(bookDto);
            var result = await _bookService.UpdateBook(book);

            if (!result)
            {
                return NotFound();
            }

            return Ok(new ApiResponse<BookDto>(bookDto));
        }

        // DELETE: api/Books/{isbn}
        [HttpDelete("{isbn}")]
        public async Task<IActionResult> DeleteBook(int Isbn)
        {
            var result = await _bookService.DeleteBook(Isbn);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}

