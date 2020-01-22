using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperMed.Migrations
{
    public partial class SpecFK3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Specializations",
                newName: "SpecializationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecializationId",
                table: "Specializations",
                newName: "Id");
        }
    }
}
