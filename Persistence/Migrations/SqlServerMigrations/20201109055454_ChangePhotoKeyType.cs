using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations.SqlServerMigrations
{
    public partial class ChangePhotoKeyType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserPhotos_UserPhotoId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPhotos",
                table: "UserPhotos");

            migrationBuilder.DropColumn(
                name: "alakyId",
                table: "UserPhotos");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UserPhotos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPhotos",
                table: "UserPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserPhotos_UserPhotoId",
                table: "Comments",
                column: "UserPhotoId",
                principalTable: "UserPhotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserPhotos_UserPhotoId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPhotos",
                table: "UserPhotos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserPhotos");

            migrationBuilder.AddColumn<string>(
                name: "alakyId",
                table: "UserPhotos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPhotos",
                table: "UserPhotos",
                column: "alakyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserPhotos_UserPhotoId",
                table: "Comments",
                column: "UserPhotoId",
                principalTable: "UserPhotos",
                principalColumn: "alakyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
