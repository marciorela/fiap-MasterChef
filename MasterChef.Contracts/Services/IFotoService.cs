using MasterChef.Domain.Entities;
using MasterChef.Domain.Types;
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

        bool SaveOrDelete(Receita receita, FotoInfo foto);

        bool Exists(Receita receita);
        
        bool Delete(Receita receita);

        FotoInfo Load(Receita receita);

    }
}
