using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluGo.Migrations
{
    /// <inheritdoc />
    public partial class addNumParcela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Parcelas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Parcelas");
        }
    }
}
