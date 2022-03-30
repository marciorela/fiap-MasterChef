using MasterChef.Domain.Dto;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Types;

namespace MasterChef.Contracts.Services
{
    public interface IReceitaService
    {
        Task<IEnumerable<ReceitaStdResponse>> GetAll(string? search);

        Task<Receita?> GetById(Guid id);

        Task<Receita> Add(Receita receita);

        Task<Receita> Update(Receita receita, FotoInfo foto);

        Task Delete(Guid id);
    }
}
