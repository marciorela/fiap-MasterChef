using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Domain.Entities
{
    public class Categoria : EntityBase
    {
        public Categoria(string titulo, string descricao)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            Descricao = descricao;
        }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }
    }
}
