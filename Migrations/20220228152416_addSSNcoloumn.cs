using Microsoft.EntityFrameworkCore.Migrations;

namespace Testing_Poc_Healthcare.Migrations
{
    public partial class addSSNcoloumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SSN",
                table: "PatientInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SSN",
                table: "PatientInfos");
        }
    }
}
