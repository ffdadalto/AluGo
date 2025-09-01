using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluGo.Migrations
{
    /// <inheritdoc />
    public partial class cpfcnpj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: new Guid("fa5f507f-1f3d-4d63-a421-0aa2dcedd132"));

            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "Locatarios",
                newName: "CpfCnpj");

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Ativo", "CriadoEm", "Email", "Senha" },
                values: new object[] { new Guid("6cc8e056-fbfe-45d3-89a6-1aba99a76eb2"), true, new DateTime(2025, 9, 1, 13, 6, 21, 384, DateTimeKind.Local).AddTicks(281), "ffdadalto@gmail.com ", "F1365188F9DE24F7594F6A0F501E3A10" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: new Guid("6cc8e056-fbfe-45d3-89a6-1aba99a76eb2"));

            migrationBuilder.RenameColumn(
                name: "CpfCnpj",
                table: "Locatarios",
                newName: "CPF");

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Ativo", "CriadoEm", "Email", "Senha" },
                values: new object[] { new Guid("fa5f507f-1f3d-4d63-a421-0aa2dcedd132"), true, new DateTime(2025, 8, 31, 22, 54, 44, 342, DateTimeKind.Local).AddTicks(8025), "ffdadalto@gmail.com ", "F1365188F9DE24F7594F6A0F501E3A10" });
        }
    }
}
