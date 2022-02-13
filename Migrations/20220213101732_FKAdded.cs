using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Testing_Poc_Healthcare.Migrations
{
    public partial class FKAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "PatientInfos",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "PatientInfos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "PatientAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientAddresses_PatientId",
                table: "PatientAddresses",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAddresses_PatientInfos_PatientId",
                table: "PatientAddresses",
                column: "PatientId",
                principalTable: "PatientInfos",
                principalColumn: "PatientID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAddresses_PatientInfos_PatientId",
                table: "PatientAddresses");

            migrationBuilder.DropIndex(
                name: "IX_PatientAddresses_PatientId",
                table: "PatientAddresses");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "PatientAddresses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "PatientInfos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "PatientInfos",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
    }
}
