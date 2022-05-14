using AutoMapper;
using MasterChef.Domain.Dto;
using MasterChef.Models.ViewModels;

namespace MasterChef.Automapper.ViewModelToDomain
{
	public class ReceitaViewModelToDomain : Profile
	{
		public ReceitaViewModelToDomain()
		{
			CreateMap<ReceitaViewModel, ReceitaCreateRequest>().ReverseMap();
			CreateMap<ReceitaViewModel, ReceitaResponse>().ReverseMap();

		}
	}
}
