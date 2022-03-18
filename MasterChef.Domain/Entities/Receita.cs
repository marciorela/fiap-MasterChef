using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Domain.Entities
{
    public class Receita : EntityBase
    {
        private readonly List<Tag> _tags = new();

        public Receita(string titulo, string descricao, string ingredientes, string modoDePreparo)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            Descricao = descricao;
            Ingredientes = ingredientes;
            ModoDePreparo = modoDePreparo;
            DataCadastro = DateTime.Now;
        }

        [Required]
        [StringLength(100)]
        public string Titulo { get; private set; }

        public DateTime DataCadastro { get; private set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; private set; }

        [Required]
        public string Ingredientes { get; private set; }

        [Required]
        public string ModoDePreparo { get; private set; }


        public IReadOnlyList<Tag> Tags => _tags;

        public void AddTag(string titulo, string descricao)
        {
            _tags.Add(new Tag(titulo, descricao));
        }
    }
}
