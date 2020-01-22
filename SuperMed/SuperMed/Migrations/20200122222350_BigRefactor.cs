using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SuperMed.Migrations
{
    public partial class BigRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AspNetUsers_ApplicationUserID",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsAbsences_Doctors_DoctorId",
                table: "DoctorsAbsences");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_ApplicationUserID",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "Patients",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Patients",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_ApplicationUserID",
                table: "Patients",
                newName: "IX_Patients_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "DoctorAbsenceId",
                table: "DoctorsAbsences",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "Doctors",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Doctors",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_ApplicationUserID",
                table: "Doctors",
                newName: "IX_Doctors_ApplicationUserId");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "DoctorsAbsences",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SpecializationId",
                table: "Doctors",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Appointments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Appointments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AppointmentStatus",
                table: "Appointments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AspNetUsers_ApplicationUserId",
                table: "Doctors",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                table: "Doctors",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsAbsences_Doctors_DoctorId",
                table: "DoctorsAbsences",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_ApplicationUserId",
                table: "Patients",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AspNetUsers_ApplicationUserId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorsAbsences_Doctors_DoctorId",
                table: "DoctorsAbsences");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AspNetUsers_ApplicationUserId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "AppointmentStatus",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Patients",
                newName: "ApplicationUserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Patients",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_ApplicationUserId",
                table: "Patients",
                newName: "IX_Patients_ApplicationUserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DoctorsAbsences",
                newName: "DoctorAbsenceId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Doctors",
                newName: "ApplicationUserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Doctors",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_ApplicationUserId",
                table: "Doctors",
                newName: "IX_Doctors_ApplicationUserID");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "DoctorsAbsences",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SpecializationId",
                table: "Doctors",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Appointments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    DoctorId = table.Column<int>(nullable: true),
                    PatientId = table.Column<int>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    PropertyNumber = table.Column<int>(nullable: true),
                    StreetNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Adresses_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adresses_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_DoctorId",
                table: "Adresses",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Adresses_PatientId",
                table: "Adresses",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AspNetUsers_ApplicationUserID",
                table: "Doctors",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                table: "Doctors",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorsAbsences_Doctors_DoctorId",
                table: "DoctorsAbsences",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AspNetUsers_ApplicationUserID",
                table: "Patients",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
