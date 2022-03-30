using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Domain.Dto
{
    public class ReceitaStdResponse
    {
        public Guid Id { get; set; }

        public string? Titulo { get; set; }

        public string? Descricao { get; set; }

        public string? FotoContent { get; set; }
    }
}
