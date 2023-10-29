namespace Tarefas.API.Interfaces
{
	public interface IQuery<T>
	{
		IEnumerable<T> ObterTodos();
		T ObterPorId(int id);
		void SalvarNovo(T entity);
		void Atualizar(T entity);
		void Excluir(T entity);
	}
}
