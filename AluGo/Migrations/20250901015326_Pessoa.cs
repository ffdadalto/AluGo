using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluGo.Migrations
{
    /// <inheritdoc />
    public partial class Pessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Ativo", "CriadoEm", "Email", "Nome", "Senha" },
                values: new object[] { new Guid("79b3727d-9861-4e4f-b888-05e4dd6f3aa3"), true, new DateTime(2025, 8, 31, 22, 53, 25, 882, DateTimeKind.Local).AddTicks(1652), "ffdadalto@gmail.com ", "", "F1365188F9DE24F7594F6A0F501E3A10" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
