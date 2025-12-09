using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hulujan_Iulia_Petruta_proiectNouM.Migrations
{
    /// <inheritdoc />
    public partial class MakeDepartmentOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Department_DepartmentID",
                table: "Student");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "Student",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Department_DepartmentID",
                table: "Student",
                column: "DepartmentID",
                principalTable: "Department",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Department_DepartmentID",
                table: "Student");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Department_DepartmentID",
                table: "Student",
                column: "DepartmentID",
                principalTable: "Department",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
