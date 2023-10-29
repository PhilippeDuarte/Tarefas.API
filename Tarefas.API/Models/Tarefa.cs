using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tarefas.API.Models
{
	public class Tarefa
	{		
		public int TarefaId { get; set; }
		[Required(ErrorMessage = "O Título é obrigatório")]
		[StringLength(80, ErrorMessage = "O Título deve ter no máximo 80 caracteres.")]
		public string Titulo { get; set; }
		[Required]
		[StringLength(80, ErrorMessage = "A Descrição deve ter no máximo 300 80 caracteres.")]
		public string Descricao { get;set; }
		public DateTime DataCriacao { get; set; }
		public EnumStatus Status { get; set; }
		[StringLength(256)]
		[JsonIgnore]
		
		public string? Usuario{ get; set; }
	}
}
