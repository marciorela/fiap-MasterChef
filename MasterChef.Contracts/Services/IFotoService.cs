using MasterChef.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Contracts.Services
{
    public interface IFotoService
    {

        Task<bool> Save(IFormFile foto, Guid id);

        bool Save(IFormFile foto, Receita receita);

        bool Exists(Receita receita);
        
        bool Delete(Receita receita);

    }
}
