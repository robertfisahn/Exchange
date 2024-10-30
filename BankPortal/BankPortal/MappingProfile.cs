using AutoMapper;
using BankPortal.Entities;
using BankPortal.Models.Dto;

namespace BankPortal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Wallet, WalletDto>();
            CreateMap<WalletTokenDto, WalletTokenReadDto>();
            CreateMap<OrderDto, TokenUpdateDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TokenName));
            CreateMap<TokenDto, WalletTokenReadDto>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.TotalSupply, opt => opt.MapFrom(src => src.TotalSupply))
            .ForMember(dest => dest.MarketCap, opt => opt.MapFrom(src => src.MarketCap));
        }
    }
}
