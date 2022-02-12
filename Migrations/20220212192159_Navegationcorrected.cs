using Microsoft.EntityFrameworkCore.Migrations;

namespace Testing_Poc_Healthcare.Migrations
{
    public partial class Navegationcorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientInfos_PatientAddresses_AddressPatientAddressID",
                table: "PatientInfos");

            migrationBuilder.DropIndex(
                name: "IX_PatientInfos_AddressPatientAddressID",
                table: "PatientInfos");

            migrationBuilder.DropColumn(
                name: "AddressPatientAddressID",
                table: "PatientInfos");

            migrationBuilder.AddColumn<int>(
                name: "PatientID1",
                table: "PatientAddresses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientAddresses_PatientID1",
                table: "PatientAddresses",
                column: "PatientID1");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAddresses_PatientInfos_PatientID1",
                table: "PatientAddresses",
                column: "PatientID1",
                principalTable: "PatientInfos",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAddresses_PatientInfos_PatientID1",
                table: "PatientAddresses");

            migrationBuilder.DropIndex(
                name: "IX_PatientAddresses_PatientID1",
                table: "PatientAddresses");

            migrationBuilder.DropColumn(
                name: "PatientID1",
                table: "PatientAddresses");

            migrationBuilder.AddColumn<int>(
                name: "AddressPatientAddressID",
                table: "PatientInfos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientInfos_AddressPatientAddressID",
                table: "PatientInfos",
                column: "AddressPatientAddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientInfos_PatientAddresses_AddressPatientAddressID",
                table: "PatientInfos",
                column: "AddressPatientAddressID",
                principalTable: "PatientAddresses",
                principalColumn: "PatientAddressID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
