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
    public class ReceitaRepository : RepositoryBase, IReceitaRepository
    {
        public ReceitaRepository(MainDbContext ctx) : base(ctx)
        {
        }

        public async Task<List<Receita>> GetAll(string? search)
        {
            search ??= string.Empty;
            search = "%" + search + "%";

            return await _ctx.Receitas
                .Where(x => EF.Functions.Like(x.Titulo ?? "", search)
                    || EF.Functions.Like(x.Descricao ?? "", search)
                    || EF.Functions.Like(x.Ingredientes ?? "", search)
                )
                .AsNoTracking()
                //.Select(r => new ReceitaListVM() { Id = r.Id, Titulo = r.Titulo })
                .ToListAsync();
        }

        public async Task<Receita?> GetById(Guid id)
        {
            return await _ctx.Receitas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(Receita receita)
        {
            await _ctx.Receitas.AddAsync(receita);
            await _ctx.SaveChangesAsync();
        }

        public async Task Update(Receita receita)
        {
            _ctx.Receitas.Update(receita);

            _ctx.Entry(receita).Property(p => p.DataCadastro).IsModified = false;
            await _ctx.SaveChangesAsync();
        }

        public async Task Delete(Receita receita)
        {
            _ctx.Receitas.Remove(receita);
            await _ctx.SaveChangesAsync();
        }
    }
}
