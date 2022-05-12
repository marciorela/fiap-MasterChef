using System.ComponentModel.DataAnnotations;

namespace MasterChef.Models.ViewModels
{
	public class CreateReceitaViewModel
	{
		[Required(ErrorMessage = "Título deve ser informado.")]
		[StringLength(100, ErrorMessage = "Tamanho do campo não deve ultrapassar %1 caracteres.")]
		[Display(Name = "Título:")]
		public string? Titulo { get; set; }

		[Required(ErrorMessage = "Descrição deve ser informada.")]
		[StringLength(100, ErrorMessage = "Tamanho do campo não deve ultrapassar %1 caracteres.")]
		[Display(Name = "Descrição:")]
		public string? Descricao { get; set; }

		[Required(ErrorMessage = "Ingredientes devem ser informados.")]
		[Display(Name = "Ingredientes:")]
		public string? Ingredientes { get; set; }

		[Required(ErrorMessage = "Modo de preparo deve ser informado.")]
		[Display(Name = "Modo de Preparo:")]
		public string? ModoDePreparo { get; set; }

		[Display(Name = "Foto:")]
		public string? Foto { get; set; }
	}
}
