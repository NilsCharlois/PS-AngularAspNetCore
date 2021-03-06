using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;

namespace DutchTreat.Data
{
    public class DutchMappingProfile : Profile
    {
        public DutchMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
            .ForMember(o=>o.OrderId, ex=>ex.MapFrom(o=>o.Id))
            .ReverseMap(); // ReverMap to reverse the mapping

            CreateMap<OrderItem, OrderItemViewModel>()
            .ReverseMap();
        }

    }
}