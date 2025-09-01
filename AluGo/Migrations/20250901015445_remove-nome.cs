using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluGo.Migrations
{
    /// <inheritdoc />
    public partial class removenome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: new Guid("79b3727d-9861-4e4f-b888-05e4dd6f3aa3"));

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Pessoas");

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Ativo", "CriadoEm", "Email", "Senha" },
                values: new object[] { new Guid("fa5f507f-1f3d-4d63-a421-0aa2dcedd132"), true, new DateTime(2025, 8, 31, 22, 54, 44, 342, DateTimeKind.Local).AddTicks(8025), "ffdadalto@gmail.com ", "F1365188F9DE24F7594F6A0F501E3A10" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: new Guid("fa5f507f-1f3d-4d63-a421-0aa2dcedd132"));

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Pessoas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Ativo", "CriadoEm", "Email", "Nome", "Senha" },
                values: new object[] { new Guid("79b3727d-9861-4e4f-b888-05e4dd6f3aa3"), true, new DateTime(2025, 8, 31, 22, 53, 25, 882, DateTimeKind.Local).AddTicks(1652), "ffdadalto@gmail.com ", "", "F1365188F9DE24F7594F6A0F501E3A10" });
        }
    }
}
