using AutoMapper;
using ShoeShop.Core.Dto;
using ShoeShop.Core.Models;

namespace ShoeShop.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderDto, Order>();
            CreateMap<ShoeDto, Shoe>();

            CreateMap<Shoe, ShoeDto>();
        }
    }
}
