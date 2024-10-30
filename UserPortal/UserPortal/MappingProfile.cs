using AutoMapper;
using UserPortal.Entities;
using UserPortal.Models.Dto;
using UserPortal.Models.Dto.Account;

namespace UserPortal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserDto, User>();
            CreateMap<CreateOrderDto, OrderDto>();
        }
    }
}
