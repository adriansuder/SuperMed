using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperMed.Migrations
{
    public partial class AddUniid2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniId",
                table: "Appointments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniId",
                table: "Appointments");
        }
    }
}
