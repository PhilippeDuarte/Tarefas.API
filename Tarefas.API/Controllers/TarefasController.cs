using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using Tarefas.API.Interfaces;
using Tarefas.API.Models;

namespace Tarefas.API.Controllers
{
	[Authorize(AuthenticationSchemes ="Bearer")]
	[Route("api/[controller]")]
	[ApiController]
	public class TarefasController : ControllerBase
	{
		private readonly ITarefas _tarefas;
		public TarefasController(ITarefas tarefas)
		{
			_tarefas = tarefas;
		}
		[HttpGet]
		public ActionResult<IEnumerable<Tarefa>> ObterTodasAsTarefas()
		{
			var result = _tarefas.ObterTodos();
			if (result.Count() == 0)
			{
				return NotFound("Não existem tarefas a serem listadas.");
			}
			return Ok(result);
		}
		[HttpGet("{id:int}")]
		public ActionResult<Tarefa> ObterTarefaPorId(int id)
		{
			var result = _tarefas.ObterPorId(id);
			if (result == null)
			{
				return NotFound("Não foi possível encontrar a tarefa solicitada.");
			}
			return Ok(result);
		}
		[HttpPost]
		public ActionResult<Tarefa> Post(Tarefa tarefa)
		{
			if (tarefa == null)
			{
				return BadRequest();
			}
			_tarefas.SalvarNovo(tarefa);
			return Ok();
		}
		[HttpPut]
		public ActionResult<Tarefa> Put(Tarefa tarefa)
		{
			_tarefas.Atualizar(tarefa);
			return Ok();
		}
		[HttpDelete]
		public ActionResult Delete(int id)
		{
			var result = _tarefas.ObterPorId(id);
			if (result == null)
			{
				return BadRequest();
			}
			_tarefas.Excluir(result);
			return Ok();
		}
		[HttpGet("{status:int}")]
		public ActionResult<IEnumerable<Tarefa>> ObterTarefaPorStatus(EnumStatus status)
		{
			var result = _tarefas.obterTarefasPorStatus(status);
			if (result == null)
			{
				return BadRequest("Não existem tarefas com o Status selecionado.");
			}
			return Ok(result);
		}
	}
}
