using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaInventario.DAO.Migrations
{
    /// <inheritdoc />
    public partial class CambioDeNombrePaisMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "País",
                table: "AspNetUsers",
                newName: "Pais");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pais",
                table: "AspNetUsers",
                newName: "País");
        }
    }
}
