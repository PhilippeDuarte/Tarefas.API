using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tarefas.API.Migrations
{
    /// <inheritdoc />
    public partial class AjusteUsuarioTareda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "Tarefas",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "Tarefas");
        }
    }
}
