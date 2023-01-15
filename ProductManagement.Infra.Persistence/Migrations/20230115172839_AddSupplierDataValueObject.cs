using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagement.Infra.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierDataValueObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierDescription",
                table: "Products",
                newName: "SupplierData_SupplierDescription");

            migrationBuilder.RenameColumn(
                name: "SupplierCode",
                table: "Products",
                newName: "SupplierData_SupplierCode");

            migrationBuilder.RenameColumn(
                name: "SupplierCnpj",
                table: "Products",
                newName: "SupplierData_SupplierCnpj");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierData_SupplierDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SupplierData_SupplierCnpj",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierData_SupplierDescription",
                table: "Products",
                newName: "SupplierDescription");

            migrationBuilder.RenameColumn(
                name: "SupplierData_SupplierCode",
                table: "Products",
                newName: "SupplierCode");

            migrationBuilder.RenameColumn(
                name: "SupplierData_SupplierCnpj",
                table: "Products",
                newName: "SupplierCnpj");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierCnpj",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
