using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagement.Infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterPropertyNameCnpj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierCNPJ",
                table: "Products",
                newName: "SupplierCnpj");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierCnpj",
                table: "Products",
                newName: "SupplierCNPJ");
        }
    }
}
