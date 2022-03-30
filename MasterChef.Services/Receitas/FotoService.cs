using MasterChef.Contracts.Data;
using MasterChef.Contracts.Services;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Services.Receitas
{
    public class FotoService : IFotoService
    {
        private readonly IConfiguration _config;
        private readonly IReceitaRepository _receitaRepository;
        private readonly string _path;

        public FotoService(
            IConfiguration config,
            IReceitaRepository receitaRepository
            )
        {
            _config = config;
            _receitaRepository = receitaRepository;

            _path = _config.GetSection("Folders:Photo").Value;
        }

        private string FileName(Receita receita)
        {
            return Path.Combine(_path, receita.Id.ToString());
        }

        public bool Delete(Receita receita)
        {
            try
            {
                if (Exists(receita))
                {
                    File.Delete(FileName(receita));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Exists(Receita receita)
        {
            return File.Exists(FileName(receita));
        }

        public bool Save(IFormFile foto, Receita receita)
        {
            try
            {
                var nome = FileName(receita);
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }

                using var stream = System.IO.File.Create(nome);
                foto.CopyToAsync(stream);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveOrDelete(Receita receita, FotoInfo foto)
        {
            if (string.IsNullOrWhiteSpace(foto.ContentBase64))
            {
                Delete(receita);

                return false;
            }
            else
            {
                var content = Convert.FromBase64String(foto.ContentBase64);
                File.WriteAllBytes(FileName(receita), content);

                return true;
            }
        }

        public FotoInfo Load(Receita receita)
        {
            var filename = FileName(receita);

            if (File.Exists(filename))
            {
                return new FotoInfo(
                    FileName(receita),
                    Convert.ToBase64String(File.ReadAllBytes(filename))
                );
            }
            else
            {
                return new FotoInfo();
            }
        }
    }
}
