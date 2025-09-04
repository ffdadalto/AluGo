using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluGo.Migrations
{
    /// <inheritdoc />
    public partial class correcaorecibo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: new Guid("c891e929-90d0-4e6a-b0c7-ded3858b0b8a"));

            migrationBuilder.DropColumn(
                name: "CaminhoArquivo",
                table: "Recibos");

            migrationBuilder.DropColumn(
                name: "UltimaEdicao",
                table: "Recibos");

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Ativo", "CriadoEm", "Email", "Senha", "UltimaEdicao" },
                values: new object[] { new Guid("89c075d9-3b60-40fd-bdc6-1a977e290aba"), true, new DateTime(2025, 9, 3, 22, 36, 25, 177, DateTimeKind.Local).AddTicks(5238), "ffdadalto@gmail.com ", "F1365188F9DE24F7594F6A0F501E3A10", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: new Guid("89c075d9-3b60-40fd-bdc6-1a977e290aba"));

            migrationBuilder.AddColumn<string>(
                name: "CaminhoArquivo",
                table: "Recibos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaEdicao",
                table: "Recibos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Ativo", "CriadoEm", "Email", "Senha", "UltimaEdicao" },
                values: new object[] { new Guid("c891e929-90d0-4e6a-b0c7-ded3858b0b8a"), true, new DateTime(2025, 9, 1, 21, 27, 12, 378, DateTimeKind.Local).AddTicks(8001), "ffdadalto@gmail.com ", "F1365188F9DE24F7594F6A0F501E3A10", null });
        }
    }
}
