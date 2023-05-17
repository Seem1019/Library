using AutoMapper;
using Library.Core.DTOs;
using Library.Core.Entities;
using System.Linq;

namespace Library.Infrastructure.Mappings
{


    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();

            CreateMap<AuthorHasBook, AuthorHasBookDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));
            CreateMap<AuthorHasBookDto, AuthorHasBook>()
                .ForPath(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForPath(dest => dest.Book, opt => opt.MapFrom(src => src.Book));

            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.AuthorHasBooks.Select(ahb => ahb.Author)));
            CreateMap<BookDto, Book>()
                .ForMember(dest => dest.AuthorHasBooks, opt => opt.MapFrom(src => src.Authors.Select(a => new AuthorHasBook { AuthorId = a.Id, BookIsbn = src.Isbn })));

            CreateMap<Publisher, PublisherDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
            CreateMap<PublisherDto, Publisher>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));

            CreateMap<Security, SecurityDto>();
            CreateMap<SecurityDto, Security>();
        }
    }
}
