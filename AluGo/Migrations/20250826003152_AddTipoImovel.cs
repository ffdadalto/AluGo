using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluGo.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoImovel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Imoveis",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Imoveis");
        }
    }
}
