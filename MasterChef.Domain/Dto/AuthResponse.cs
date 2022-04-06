using System.ComponentModel.DataAnnotations;

namespace MasterChef.Domain.Dto
{
    public class AuthResponse
    {
        /// <summary>Token de Sessão</summary>
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        [MaxLength(500, ErrorMessage = "Tamanho máximo de {1} caracteres")]
        public string Token { get; set; }

        /// <summary>Data e Hora do início da Sessão</summary>
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public DateTime StartedAt { get; set; }

        /// <summary>Tempo em segungos da validade do Token</summary>
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public int ExpiresIn { get; set; }

        public AuthResponse(string token, DateTime startedAt, int expiresIn)
        {
            this.Token = token;
            this.StartedAt = startedAt;
            this.ExpiresIn = expiresIn;
        }

    }
}
