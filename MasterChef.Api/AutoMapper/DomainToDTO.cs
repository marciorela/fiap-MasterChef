using AutoMapper;
using MasterChef.Domain.Dto;
using MasterChef.Domain.Entities;

namespace MasterChef.Api.AutoMapper
{
    public class DomainToDTO : Profile
    {
        public DomainToDTO()
        {
            CreateMap<ReceitaResponse, Receita>().ReverseMap();
            CreateMap<ReceitaCreateRequest, Receita>().ReverseMap();            

        }
        
    }
}
