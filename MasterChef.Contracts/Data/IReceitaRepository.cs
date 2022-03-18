using MasterChef.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Contracts.Data
{
    public interface IReceitaRepository
    {

        Task<List<Receita>> GetAll(string? search);
        
        Task<Receita?> GetById(Guid id);

        Task Add(Receita receita);

        Task Update(Receita receita);

        Task Delete(Receita receita);
            
    }
}
