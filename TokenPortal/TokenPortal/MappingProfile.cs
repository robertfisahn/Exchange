using AutoMapper;
using TokenPortal.Dto.Models;
using TokenPortal.Entities;
using TokenPortal.Models.Dto;

namespace TokenPortal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TokenDto, Token>();
            CreateMap<Token, TokenDto>();
            CreateMap<TokenCreateDto, Token>();
        }
    }
}
