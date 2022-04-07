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


            //CreateMap<CreateReceitaViewModel, ReceitaCreateRequest>().ReverseMap();
            //CreateMap<CreateReceitaViewModel, Receita>().ReverseMap();
            CreateMap<CreateReceitaViewModel, ReceitaCreateRequest>().
                ConvertUsing((source, dest) =>
                 {
                     ReceitaCreateRequest result = dest ?? new ReceitaCreateRequest();
                     if (source != null)
                     {
                         result.FotoName = System.IO.Path.GetFileName(source.FileName);
                         result.FotoContent = source.ContentType;
                         using (var reader = new System.IO.BinaryReader(source.InputStream))
                             result.FotoContent = reader.ReadBytes(source.ContentLength);
                     }
                     return result;
                 });
        }
        
    }
}
