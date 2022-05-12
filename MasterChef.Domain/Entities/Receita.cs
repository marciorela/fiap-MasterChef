using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Domain.Entities
{
    public class Receita : EntityBase
    {
        public Receita() { }

        public Receita(string? titulo, string? descricao, string? ingredientes, string? modoDePreparo)
        {
            Titulo = titulo;
            Descricao = descricao;
            Ingredientes = ingredientes;
            ModoDePreparo = modoDePreparo;

            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
        }

        [StringLength(100)]
        [Required(ErrorMessage = "Título deve ser informado.")]
        [Display(Name = "Título")]
        public string? Titulo { get; init; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; init; }

        [Required(ErrorMessage = "Descrição deve ser informada.")]
        [StringLength(100)]
        [Display(Name = "Descrição")]
        public string? Descricao { get; init; }

        [Required(ErrorMessage = "Ingredientes devem ser informados.")]
        [Display(Name = "Ingredientes")]
        public string? Ingredientes { get; init; }

        [Required(ErrorMessage = "Modo de Preparo deve ser informado.")]
        [Display(Name = "Modo de Preparo")]
        public string? ModoDePreparo { get; init; }

        [Display(Name = "Imagem")]
        [StringLength(255)]
        public string? Foto { get; set; }

        [Display(Name = "Tags")]
        [StringLength(255)]
        public string? Tags { get; set; }

        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }

    }

    public class ReceitaEntity : EntityBase
    {
        protected ReceitaEntity() { }

        public ReceitaEntity(string titulo, string descricao, string ingredientes, string modoDePreparo)
        {
            Titulo = titulo;
            Descricao = descricao;
            Ingredientes = ingredientes;
            ModoDePreparo = modoDePreparo;

            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
        }

        [StringLength(100)]
        [Required(ErrorMessage = "Título deve ser informado.")]
        [Display(Name = "Título")]
        public string Titulo { get; private set; } = string.Empty;

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; init; }

        [Required(ErrorMessage = "Descrição deve ser informada.")]
        [StringLength(100)]
        [Display(Name = "Descrição")]
        public string Descricao { get; private set; } = string.Empty;

        [Required(ErrorMessage = "Ingredientes devem ser informados.")]
        [Display(Name = "Ingredientes")]
        public string Ingredientes { get; private set; } = string.Empty;

        [Required(ErrorMessage = "Modo de Preparo deve ser informado.")]
        [Display(Name = "Modo de Preparo")]
        public string ModoDePreparo { get; private set; } = string.Empty;
    }
}
