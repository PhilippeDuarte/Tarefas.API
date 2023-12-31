﻿using Tarefas.API.Context;
using Tarefas.API.Models;

namespace Tarefas.API.Interfaces.Services
{
	public class TarefasService : ITarefas
	{
		private AppDbContext _context;
		public TarefasService()
		{

		}
		public TarefasService(AppDbContext context)
		{
			_context = context;
		}

		public virtual void Atualizar(Tarefa entity)
		{
			_context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_context.SaveChanges();
		}

		public virtual void Excluir(Tarefa entity)
		{
			_context.Tarefas.Remove(entity);
			_context.SaveChanges();
		}

		public virtual Tarefa ObterPorId(int id)
		{
			return _context.Tarefas.FirstOrDefault(t => t.TarefaId == id);
		}

		public virtual IEnumerable<Tarefa> ObterTarefasPorStatus(EnumStatus status)
		{
			return _context.Tarefas.Where(s=> s.Status== status).ToList();
		}

		public virtual IEnumerable<Tarefa> ObterTodos()
		{
			return _context.Tarefas.ToList();
		}

		public virtual void SalvarNovo(Tarefa entity)
		{
			_context.Tarefas.Add(entity);
			_context.SaveChanges();
		}

		public virtual bool verificaPermissaoUsuarioTarefa(int tarefaId, string usuario)
		{
			var result = _context.Tarefas.Where(t => t.TarefaId == tarefaId).Where(u=> u.Usuario== usuario);
			return result.Count() > 0 ? true : false;
		}
	}
}
