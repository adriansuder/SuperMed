using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperMed.Migrations
{
    public partial class doctorFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AbsenceDescription",
                table: "DoctorsAbsences",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsenceDescription",
                table: "DoctorsAbsences");
        }
    }
}
