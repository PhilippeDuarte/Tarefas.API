using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarefas.API.Interfaces;
using Tarefas.API.Models;

namespace Tarefas.API.Controllers
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[Route("api/[controller]")]
	[ApiController]
	public class TarefasController : ControllerBase
	{
		private readonly ITarefas _tarefas;
		private readonly IConfiguration _configuration;
		private readonly IDecripta _decripta;
		private readonly ILogger<TarefasController> _logger;
		public TarefasController(ITarefas tarefas, IConfiguration configuration, IDecripta decripta)
		{
			_tarefas = tarefas;
			_configuration = configuration;
			_decripta = decripta;
		}

		/// <summary>
		/// Exibe a lista de tarefas completa
		/// </summary>
		/// <param name="id"></param>		
		/// <returns>Status 200 e Objeto Tarefa</returns>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Produces("application/json")]
		[HttpGet("ObterTodasASTarefas")]
		public ActionResult<IEnumerable<Tarefa>> ObterTodasAsTarefas()
		{
			var result = _tarefas.ObterTodos();
			if (result.Count() == 0)
			{
				return NotFound("Não existem tarefas a serem listadas.");
			}
			return Ok(result);
		}

		/// <summary>
		/// Obtem a Tarefa pelo seu Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Status 200 e Objeto Tarefa</returns>
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[Produces("application/json")]
		[HttpGet("ObterTarefasPorId")]
		public ActionResult<Tarefa> ObterTarefaPorId(int id)
		{
			Tarefa result = _tarefas.ObterPorId(id);
			if (result == null)
			{
				return NotFound("Não foi possível encontrar a tarefa solicitada.");
			}
			return result;
		}

		/// <summary>
		/// Inclui uma nova tarefa
		/// </summary>
		/// <param name="tarefa"></param>
		/// <remarks>
		///		Exemplo de Requisicao:
		///		{
		///			"titulo": "Teste Validacao usuario 1",
		///			"descricao": "Teste Validacao usuario tarefa 1",
		///			"dataCriacao": "2023-10-29T20:32:31.954",
		///			"status": 1
		///		}
		///	</remarks>
		///	
		/// <returns>Status 200</returns>
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpPost("CriarTarefa")]
		public ActionResult<Tarefa> Post(Tarefa tarefa)
		{
			string usuario = _decripta.DecriptaUsuario(Request.Headers["Authorization"]);
			if (tarefa == null)
			{
				return BadRequest();
			}
			tarefa.Usuario = usuario;
			_tarefas.SalvarNovo(tarefa);
			return Ok();

		}

		/// <summary>
		/// Atualiza os dados do objeto Tarefa
		/// </summary>
		/// <param name="id"></param>
		/// <remarks>
		///		Exemplo de Requisicao:
		///		{
		///			"tarefaId": 3,
		///			"titulo": "Teste Validacao usuario 1",
		///			"descricao": "Teste Validacao usuario tarefa 1",
		///			"dataCriacao": "2023-10-29T20:32:31.954",
		///			"status": 1
		///		}
		/// </remarks>
		/// <returns>Status 200</returns>
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[HttpPut("AlterarTarefa")]
		public ActionResult<Tarefa> Put(Tarefa tarefa)
		{
			var result = _tarefas.ObterPorId(tarefa.TarefaId);
			if (result == null)
			{
				return BadRequest("Não foi possível encontrar a tarefa solicitada.");
			}
			string usuario = _decripta.DecriptaUsuario(Request.Headers["Authorization"]);
			if (_tarefas.verificaPermissaoUsuarioTarefa(tarefa.TarefaId, usuario))
			{
				tarefa.Usuario = usuario;
				_tarefas.Atualizar(tarefa);
				return Ok();
			}

			return Unauthorized("Usuário sem permissão para excluir a tarefa selecionada.");
		}

		/// <summary>
		/// Exclui uma tarefa do banco de dados.
		/// </summary>
		/// <param name="id"></param>
		/// <remarks>
		///		É necessario informar o código da tarefa. 
		/// </remarks>
		/// 
		/// <returns>Status 200</returns>
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpDelete("ExcluirTarefa")]
		public ActionResult Delete(int id)
		{
			string usuario = _decripta.DecriptaUsuario(Request.Headers["Authorization"]);
			if (_tarefas.verificaPermissaoUsuarioTarefa(id, usuario))
			{
				var result = _tarefas.ObterPorId(id);
				if (result == null)
				{
					return BadRequest();
				}
				_tarefas.Excluir(result);
				return Ok();
			}

			return Unauthorized("Usuário sem permissão para excluir a tarefa selecionada");
		}

		/// <summary>
		/// Exibe a lista de tarefas baseada em seu status
		/// </summary>
		/// <param name="id"></param>
		/// <remarks> 
		/// Status possíveis: 
		/// 0 - Pendente; 
		/// 1 - Em Andamento; 
		/// 2 - Concluida.
		/// 
		/// </remarks>
		/// <returns>Status 200 e Objeto Tarefa</returns>
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[HttpGet("ObterTarefasPorStatus")]
		public ActionResult<IEnumerable<Tarefa>> ObterTarefasPorStatus([FromHeader] EnumStatus status)
		{
			var result = _tarefas.ObterTarefasPorStatus(status);
			if (result == null)
			{
				return BadRequest("Não existem tarefas com o Status selecionado.");
			}
			return Ok(result);
		}
	}
}
