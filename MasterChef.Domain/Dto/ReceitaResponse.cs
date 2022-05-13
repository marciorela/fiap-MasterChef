using MasterChef.Domain.Entities;

namespace MasterChef.Domain.Dto
{
	public class ReceitaResponse : Receita
	{
		public string? FotoContent { get; set; }
	}
}
