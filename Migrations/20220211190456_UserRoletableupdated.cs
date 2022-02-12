using Microsoft.EntityFrameworkCore.Migrations;

namespace Testing_Poc_Healthcare.Migrations
{
    public partial class UserRoletableupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_UserRoles_UserRoleId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserRoleId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "Roles");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "UserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserRoles");

            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserRoleId",
                table: "Roles",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_UserRoles_UserRoleId",
                table: "Roles",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "UserRoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
