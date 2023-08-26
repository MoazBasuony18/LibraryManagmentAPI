using AutoMapper;
using LibraryManagmentAPI.Common.DTOs;
using LibraryManagmentAPI.Domain.Entities;

namespace LibraryManagmentAPI.Common.MappingHelper
{
    public class MapperInit : Profile
    {
        public MapperInit()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book, CreateBookDTO>().ReverseMap();
            CreateMap<Book, UpdateBookDTO>().ReverseMap();
            CreateMap<Checkout, CheckoutDTO>().ReverseMap();
            CreateMap<Checkout, CreateCheckoutDTO>().ReverseMap();
            //CreateMap<Checkout, CheckoutDTO>()
            //              .ForMember(t => t.UserId, tr => tr.MapFrom(x => x.UserId))
            //              .ForMember(t => t.DueDate, tr => tr.MapFrom(x => x.DueDate))
            //              .ForMember(t => t.CheckoutDate, tr => tr.MapFrom(x => x.CheckoutDate))
            //              .ForMember(t => t.BookId, tr => tr.MapFrom(x => x.BookId))
            //              .ForMember(t => t.Id, tr => tr.MapFrom(x => x.Id));

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserDataDTO>().ReverseMap();
        }
    }
}
