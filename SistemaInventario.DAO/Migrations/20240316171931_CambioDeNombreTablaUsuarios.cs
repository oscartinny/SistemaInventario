using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaInventario.DAO.Migrations
{
    /// <inheritdoc />
    public partial class CambioDeNombreTablaUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombres",
                table: "AspNetUsers",
                newName: "Ciudad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ciudad",
                table: "AspNetUsers",
                newName: "Nombres");
        }
    }
}
