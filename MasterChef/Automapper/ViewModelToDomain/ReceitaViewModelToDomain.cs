using AutoMapper;
using MasterChef.Domain.Dto;
using MasterChef.Domain.Entities;
using MasterChef.Models.ViewModels;

namespace MasterChef.Automapper.ViewModelToDomain
{
    public class ReceitaViewModelToDomain : Profile
    {
        public ReceitaViewModelToDomain()
        {


            CreateMap<CreateReceitaViewModel, ReceitaCreateRequest>().ReverseMap();
            //CreateMap<CreateReceitaViewModel, Receita>().ReverseMap();

        }
    }
}
