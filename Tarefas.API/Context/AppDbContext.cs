using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tarefas.API.Models;

namespace Tarefas.API.Context
{
	public class AppDbContext : IdentityDbContext
	{
			public DbSet<Tarefa> Tarefas { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlite("DataSource=tarefa.db; Cache=Shared");

	}
}
