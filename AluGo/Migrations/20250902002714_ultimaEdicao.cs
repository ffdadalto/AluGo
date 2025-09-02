using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluGo.Migrations
{
    /// <inheritdoc />
    public partial class ultimaEdicao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: new Guid("bf42432b-f9f8-43c1-9da2-5d3ce7af0302"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaEdicao",
                table: "Recibos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaEdicao",
                table: "Recebimentos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaEdicao",
                table: "Reajustes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaEdicao",
                table: "Pessoas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaEdicao",
                table: "Parcelas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaEdicao",
                table: "Locatarios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaEdicao",
                table: "Imoveis",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaEdicao",
                table: "Contratos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Ativo", "CriadoEm", "Email", "Senha", "UltimaEdicao" },
                values: new object[] { new Guid("c891e929-90d0-4e6a-b0c7-ded3858b0b8a"), true, new DateTime(2025, 9, 1, 21, 27, 12, 378, DateTimeKind.Local).AddTicks(8001), "ffdadalto@gmail.com ", "F1365188F9DE24F7594F6A0F501E3A10", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: new Guid("c891e929-90d0-4e6a-b0c7-ded3858b0b8a"));

            migrationBuilder.DropColumn(
                name: "UltimaEdicao",
                table: "Recibos");

            migrationBuilder.DropColumn(
                name: "UltimaEdicao",
                table: "Recebimentos");

            migrationBuilder.DropColumn(
                name: "UltimaEdicao",
                table: "Reajustes");

            migrationBuilder.DropColumn(
                name: "UltimaEdicao",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "UltimaEdicao",
                table: "Parcelas");

            migrationBuilder.DropColumn(
                name: "UltimaEdicao",
                table: "Locatarios");

            migrationBuilder.DropColumn(
                name: "UltimaEdicao",
                table: "Imoveis");

            migrationBuilder.DropColumn(
                name: "UltimaEdicao",
                table: "Contratos");

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Ativo", "CriadoEm", "Email", "Senha" },
                values: new object[] { new Guid("bf42432b-f9f8-43c1-9da2-5d3ce7af0302"), true, new DateTime(2025, 9, 1, 19, 2, 38, 688, DateTimeKind.Local).AddTicks(2533), "ffdadalto@gmail.com ", "F1365188F9DE24F7594F6A0F501E3A10" });
        }
    }
}
