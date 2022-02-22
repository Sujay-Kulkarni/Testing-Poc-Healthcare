using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Testing_Poc_Healthcare.Migrations
{
    public partial class BenfitPlanDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientInsurances",
                columns: table => new
                {
                    PatientInsuranceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    InsuranceId = table.Column<int>(type: "int", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientInsurances", x => x.PatientInsuranceId);
                    table.ForeignKey(
                        name: "FK_PatientInsurances_InsuranceInfos_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "InsuranceInfos",
                        principalColumn: "InsuranceInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientInsurances_PatientInfos_PatientId",
                        column: x => x.PatientId,
                        principalTable: "PatientInfos",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PatientInsurances_InsuranceId",
                table: "PatientInsurances",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientInsurances_PatientId",
                table: "PatientInsurances",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientInsurances");
        }
    }
}
