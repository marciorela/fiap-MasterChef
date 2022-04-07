using System.ComponentModel.DataAnnotations;

namespace MasterChef.Domain.Entities
{
    public class Auth : EntityBase
    {
        /// <summary>ClaimId</summary>
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string? ClientId { get; set; }

        /// <summary>Secret</summary>
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(100, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string? Secret { get; set; }
    }
}
