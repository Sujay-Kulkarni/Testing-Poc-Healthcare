using Microsoft.EntityFrameworkCore.Migrations;

namespace Testing_Poc_Healthcare.Migrations
{
    public partial class UserInfoUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserRolesUserRoleId",
                table: "UserInfos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserRolesUserRoleId",
                table: "UserInfos",
                column: "UserRolesUserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_UserRoles_UserRolesUserRoleId",
                table: "UserInfos",
                column: "UserRolesUserRoleId",
                principalTable: "UserRoles",
                principalColumn: "UserRoleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_UserRoles_UserRolesUserRoleId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_UserRolesUserRoleId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "UserRolesUserRoleId",
                table: "UserInfos");
        }
    }
}
