using AutoMapper;
using SalesOrderAPI.Domain.Entities;
using SalesOrderAPI.API.Models;

namespace SalesOrderAPI.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Item, ItemDto>().ReverseMap();

            CreateMap<SalesOrder, SalesOrderDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.SalesOrderDetails));

            CreateMap<SalesOrderDto, SalesOrder>()
                .ForMember(dest => dest.Client, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

            CreateMap<SalesOrderDetail, SalesOrderDetailDto>()
                .ForMember(dest => dest.ItemCode, opt => opt.MapFrom(src => src.Item.ItemCode))
                .ForMember(dest => dest.ItemDescription, opt => opt.MapFrom(src => src.Item.Description));

            CreateMap<SalesOrderDetailDto, SalesOrderDetail>()
                .ForMember(dest => dest.Item, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrder, opt => opt.Ignore());
        }
    }
}
