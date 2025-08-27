using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluGo.Migrations
{
    /// <inheritdoc />
    public partial class Inicial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Parcelas",
                type: "char(1)",
                unicode: false,
                nullable: false,
                defaultValue: "48",
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                table: "Parcelas",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)",
                oldUnicode: false,
                oldDefaultValue: "48");
        }
    }
}
