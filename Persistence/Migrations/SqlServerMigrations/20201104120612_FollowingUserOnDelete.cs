using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations.SqlServerMigrations
{
    public partial class FollowingUserOnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowings_AspNetUsers_ObserverId",
                table: "UserFollowings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowings_AspNetUsers_TargetId",
                table: "UserFollowings");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowings_AspNetUsers_ObserverId",
                table: "UserFollowings",
                column: "ObserverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowings_AspNetUsers_TargetId",
                table: "UserFollowings",
                column: "TargetId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowings_AspNetUsers_ObserverId",
                table: "UserFollowings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFollowings_AspNetUsers_TargetId",
                table: "UserFollowings");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowings_AspNetUsers_ObserverId",
                table: "UserFollowings",
                column: "ObserverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFollowings_AspNetUsers_TargetId",
                table: "UserFollowings",
                column: "TargetId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
