using MasterChef.Contracts.Data;
using MasterChef.Database;
using MasterChef.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Data.Repositories
{
    public class CategoriaRepository : RepositoryBase, ICategoriaRepository
    {
        public CategoriaRepository(MainDbContext ctx) : base(ctx)
        {
        }

        public async Task<List<Categoria>> GetAll()
        {
            return await _ctx.Categorias.AsNoTracking().ToListAsync();
        }
    }
}
