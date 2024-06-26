using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _0002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_Employees_EmployeeId",
                table: "TodoItems");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "TodoItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_Employees_EmployeeId",
                table: "TodoItems",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_Employees_EmployeeId",
                table: "TodoItems");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "TodoItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_Employees_EmployeeId",
                table: "TodoItems",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
