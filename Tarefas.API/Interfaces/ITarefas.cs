using Tarefas.API.Models;
namespace Tarefas.API.Interfaces
{
	public interface ITarefas : IQuery<Tarefa>
	{
		IEnumerable<Tarefa> ObterTarefasPorStatus(EnumStatus status);
		bool verificaPermissaoUsuarioTarefa(int tarefaId, string usuario);
	}
}
