using Microsoft.EntityFrameworkCore.Migrations;

namespace Testing_Poc_Healthcare.Migrations
{
    public partial class UserInfoUpdated1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "UserInfoUserId",
                table: "UserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserInfoUserId",
                table: "UserRoles",
                column: "UserInfoUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_UserInfos_UserInfoUserId",
                table: "UserRoles",
                column: "UserInfoUserId",
                principalTable: "UserInfos",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_UserInfos_UserInfoUserId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_UserInfoUserId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "UserInfoUserId",
                table: "UserRoles");

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
    }
}
