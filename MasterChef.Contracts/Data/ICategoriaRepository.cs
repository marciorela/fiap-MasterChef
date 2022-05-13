using MasterChef.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Contracts.Data
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> GetAll();

    }
}
