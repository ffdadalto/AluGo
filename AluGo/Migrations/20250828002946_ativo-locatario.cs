using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluGo.Migrations
{
    /// <inheritdoc />
    public partial class ativolocatario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Locatarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Locatarios");
        }
    }
}
