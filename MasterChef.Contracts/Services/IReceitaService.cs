using MasterChef.Domain.Entities;

namespace MasterChef.Contracts.Services
{
    public interface IReceitaService
    {
        Task<IEnumerable<Receita>> GetAll(string? search);

        Task<Receita?> GetById(Guid id);

        Task<Receita> Add(Receita receita);

        Task<Receita> Update(Guid id, Receita receita);

        Task Delete(Guid id);
    }
}
