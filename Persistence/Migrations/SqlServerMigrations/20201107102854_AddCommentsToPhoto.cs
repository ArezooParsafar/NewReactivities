using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Persistence.Migrations.SqlServerMigrations
{
    public partial class AddCommentsToPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Activities_ActivityId",
                table: "Comments");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserPhotos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "UserFollowings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "ActivityId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "UserPhotoId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserPhotoId",
                table: "Comments",
                column: "UserPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Activities_ActivityId",
                table: "Comments",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_Comments_Activities_ActivityId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserPhotos_UserPhotoId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserPhotoId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserPhotos");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "UserFollowings");

            migrationBuilder.DropColumn(
                name: "UserPhotoId",
                table: "Comments");

            migrationBuilder.AlterColumn<Guid>(
                name: "ActivityId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Activities_ActivityId",
                table: "Comments",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
