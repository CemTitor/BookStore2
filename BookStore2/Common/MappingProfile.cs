using AutoMapper;
using BookStore2.Application.BookOperations.Commands.CreateBooks;
using BookStore2.Application.BookOperations.Queries.GetBookDetail;
using BookStore2.Application.BookOperations.Queries.GetBooks;
using BookStore2.Common;
using BookStore2.Entities;
using static BookStore2.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;
using static BookStore2.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;

namespace BookStoreWebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /// CreateBookModel mapping to Book object
            CreateMap<CreateBookModel, Book>();
            /// Book mapping to BookDetailViewModel object and GenreId mapping to Genre
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            /// Book mapping to BooksViewModel object and GenreId mapping to Genre
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            /// Genre mapping to GenresViewModel object
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
        }
    }

}