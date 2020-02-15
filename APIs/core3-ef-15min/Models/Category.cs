using System.ComponentModel.DataAnnotations;

namespace core3_ef_15min.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		
		[Required(ErrorMessage = "Campo obrigatório")]
		[MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
		[MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
		public string TItle { get; set; }
	}
}