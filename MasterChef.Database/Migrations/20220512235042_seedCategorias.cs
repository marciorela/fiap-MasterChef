using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterChef.Database.Migrations
{
    public partial class seedCategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descricao", "Titulo" },
                values: new object[] { new Guid("03b9f797-456a-4f8d-86e6-ed4b592f0700"), "Fritos, assados, cozidos e tudo o que você achar gostoso com farinha!", "Salgados" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descricao", "Titulo" },
                values: new object[] { new Guid("8c2efb34-27e6-4982-bfb0-c83b19288307"), "Sem glúten, sem açucar, sem lactose e sem graça!", "Saudáveis" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descricao", "Titulo" },
                values: new object[] { new Guid("9513b6a3-51af-4aa8-a43b-fe8bd999021a"), "Bolos, tortas e tudo o que você achar de gostoso com açucar!", "Doces" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("03b9f797-456a-4f8d-86e6-ed4b592f0700"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("8c2efb34-27e6-4982-bfb0-c83b19288307"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("9513b6a3-51af-4aa8-a43b-fe8bd999021a"));
        }
    }
}
