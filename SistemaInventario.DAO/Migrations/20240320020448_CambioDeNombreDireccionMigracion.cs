using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaInventario.DAO.Migrations
{
    /// <inheritdoc />
    public partial class CambioDeNombreDireccionMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dirección",
                table: "AspNetUsers",
                newName: "Direccion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "AspNetUsers",
                newName: "Dirección");
        }
    }
}
