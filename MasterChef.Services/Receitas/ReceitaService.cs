using MasterChef.Contracts.Data;
using MasterChef.Contracts.Services;
using MasterChef.Domain.Dto;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Types;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Services.Receitas
{
    public class ReceitaService : IReceitaService
    {
        private readonly IReceitaRepository _receitaRepository;
        private readonly IFotoService _fotoService;

        public ReceitaService(
            IReceitaRepository receitaRepository,
            IFotoService fotoService
            )
        {
            _receitaRepository = receitaRepository;
            _fotoService = fotoService;
        }

        public async Task<Receita> Add(Receita receita)
        {
            await _receitaRepository.Add(receita);

            return receita;
        }

        public async Task Delete(Guid id)
        {
            var receita = await _receitaRepository.GetById(id);
            if (receita != null)
            {
                await _receitaRepository.Delete(receita);
                _fotoService.Delete(receita);
            }
        }

        public async Task<IEnumerable<ReceitaStdResponse>> GetAll(string? search)
        {
            var result = await _receitaRepository.GetAll(search);
            var listStd = result.Select(x => new ReceitaStdResponse()
            {
                Id = x.Id,
                Descricao = x.Descricao,
                Titulo = x.Titulo,
                FotoContent = _fotoService.Load(x).ContentBase64
            });

            return listStd;
        }

        public async Task<Receita?> GetById(Guid id)
        {
            return await _receitaRepository.GetById(id);
        }

        public async Task<Receita> Update(Receita receita, FotoInfo foto)
        {
            var receitaAntiga = await _receitaRepository.GetById(receita.Id);
            if (receitaAntiga != null)
            {
                receita.Foto = foto.FileName;

                await _receitaRepository.Update(receita);
                _fotoService.SaveOrDelete(receita, foto);
            }

            return receita;
        }
    }
}
