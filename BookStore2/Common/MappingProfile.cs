using AutoMapper;
using BookStore2;
using BookStore2.Common;

    namespace BookStoreWebApi.Common
    {
        public class MappingProfile : Profile
        {
            public MappingProfile(){
                CreateMap<CreateBookModel, Book>();
                CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            }
        }

    }