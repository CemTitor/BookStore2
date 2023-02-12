using AutoMapper;
using BookStore2;
using BookStore2.Common;

    namespace BookStoreWebApi.Common
    {
        public class MappingProfile : Profile
        {
            public MappingProfile(){
                /// CreateBookModel mapping to Book object
                CreateMap<CreateBookModel, Book>();
                /// Book mapping to BookDetailViewModel object and GenreId mapping to Genre
                CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
                /// Book mapping to BooksViewModel object and GenreId mapping to Genre
                CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            }
        }

    }