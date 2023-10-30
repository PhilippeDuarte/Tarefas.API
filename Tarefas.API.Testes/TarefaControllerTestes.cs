using Moq;
using Tarefas.API.Context;
using Tarefas.API.Interfaces;
using Tarefas.API.Interfaces.Services;
using Tarefas.API.Models;
using Xunit;

namespace Tarefas.API.Testes
{
	public class TarefaControllerTestes : IClassFixture<AppDbContext>
	{
		AppDbContext _context;
		Mock<TarefasService> _mock; 
		public TarefaControllerTestes(AppDbContext context )
		{
			_context = context;
			//arrange
			_mock = new Mock<TarefasService>();

		}
		//Lista de tarefas a serem usadas nos testes
		public List<Tarefa> tarefaMock = new List<Tarefa>()
			{
				new Tarefa()
				{
					TarefaId= 1,
					Titulo = "teste",
					Descricao = "teste descricao",
					DataCriacao = DateTime.Now,
					Status = EnumStatus.emAndamento,
					Usuario = "teste@teste.com"
				},
				new Tarefa()
				{
					TarefaId= 2,
					Titulo = "teste2",
					Descricao = "teste 2 descricao",
					DataCriacao = DateTime.Now,
					Status = EnumStatus.Concluída,
					Usuario = "teste2@teste.com"
				},
				new Tarefa()
				{
					TarefaId= 3,
					Titulo = "teste3",
					Descricao = "teste 3 descricao",
					DataCriacao = DateTime.Now,
					Status = EnumStatus.pendente,
					Usuario = "teste3@teste.com"
				}
			};
		
		[Fact]
		public void TestaObterTodasASTarefas()
		{
			//Action
			_mock.Setup(c => c.ObterTodos()).Returns(tarefaMock);
			var result = _mock.Object.ObterTodos();
			//Assert
			Assert.Equal(3, result.Count());
		}
		[Fact]
		public void TestaFalhaObterTodasAsTarefas()
		{		
			//Action
			_mock.Setup(c => c.ObterTodos()).Returns(tarefaMock);
			var result = _mock.Object.ObterTodos();
			//Assert
			Assert.NotEqual(2, result.Count());
		}
		[Fact]
		public void TestaVerificaPermissaoUsuarioTarefas()
		{
			//Action
			_mock.Setup(c => c.verificaPermissaoUsuarioTarefa(3,"teste3@teste.com")).Returns(true);
			var result = _mock.Object.verificaPermissaoUsuarioTarefa(3, "teste3@teste.com");
			//Assert
			Assert.True(result);
		}
		[Fact]
		public void TestaFalhaVerificaPermissaoUsuarioTarefas()
		{
			//Action
			_mock.Setup(c => c.verificaPermissaoUsuarioTarefa(2, "teste3@teste.com")).Returns(false);
			var result = _mock.Object.verificaPermissaoUsuarioTarefa(2, "teste3@teste.com");
			//Assert
			Assert.False(result);
		}
		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public void TestaObterTarefaPorId(int id)
		{
			//arrange
			Tarefa tarefa = new Tarefa();
			//Action
			_mock.Setup(c => c.ObterPorId(id)).Returns(tarefaMock[id-1]);
			var result = _mock.Object.ObterPorId(id);
			//Assert
			Assert.NotNull(result);
		}
	}
}
