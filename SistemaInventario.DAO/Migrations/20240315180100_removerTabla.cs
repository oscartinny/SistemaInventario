using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaInventario.DAO.Migrations
{
    /// <inheritdoc />
    public partial class removerTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categirias",
                table: "Categirias");

            migrationBuilder.RenameTable(
                name: "Categirias",
                newName: "Categorias");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias");

            migrationBuilder.RenameTable(
                name: "Categorias",
                newName: "Categirias");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categirias",
                table: "Categirias",
                column: "Id");
        }
    }
}
