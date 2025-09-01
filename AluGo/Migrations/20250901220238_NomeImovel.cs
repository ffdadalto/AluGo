using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluGo.Migrations
{
    /// <inheritdoc />
    public partial class NomeImovel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: new Guid("6cc8e056-fbfe-45d3-89a6-1aba99a76eb2"));

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Imoveis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Ativo", "CriadoEm", "Email", "Senha" },
                values: new object[] { new Guid("bf42432b-f9f8-43c1-9da2-5d3ce7af0302"), true, new DateTime(2025, 9, 1, 19, 2, 38, 688, DateTimeKind.Local).AddTicks(2533), "ffdadalto@gmail.com ", "F1365188F9DE24F7594F6A0F501E3A10" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: new Guid("bf42432b-f9f8-43c1-9da2-5d3ce7af0302"));

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Imoveis");

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Ativo", "CriadoEm", "Email", "Senha" },
                values: new object[] { new Guid("6cc8e056-fbfe-45d3-89a6-1aba99a76eb2"), true, new DateTime(2025, 9, 1, 13, 6, 21, 384, DateTimeKind.Local).AddTicks(281), "ffdadalto@gmail.com ", "F1365188F9DE24F7594F6A0F501E3A10" });
        }
    }
}
