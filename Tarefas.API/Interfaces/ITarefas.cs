using Tarefas.API.Models;
namespace Tarefas.API.Interfaces
{
	public interface ITarefas : IQuery<Tarefa>
	{
		IEnumerable<Tarefa> obterTarefasPorStatus(EnumStatus status);
	}
}
