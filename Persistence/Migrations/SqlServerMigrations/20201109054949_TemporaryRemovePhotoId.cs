using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations.SqlServerMigrations
{
    public partial class TemporaryRemovePhotoId : Migration
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
                name: "Id",
                table: "UserPhotos");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Activities");

            migrationBuilder.AddColumn<string>(
                name: "alakyId",
                table: "UserPhotos",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ActivityId",
                table: "UserPhotos",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserPhotoId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "AccessTokenExpiresDateTime",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessTokenHash",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByBrowserName",
                table: "AspNetUserTokens",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "AspNetUserTokens",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByBrowserName",
                table: "AspNetUserTokens",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByIp",
                table: "AspNetUserTokens",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RefreshTokenExpiresDateTime",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshTokenIdHash",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshTokenIdHashSource",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "AspNetUserTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserTokens",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByBrowserName",
                table: "AspNetUserRoles",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "AspNetUserRoles",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByBrowserName",
                table: "AspNetUserRoles",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByIp",
                table: "AspNetUserRoles",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByBrowserName",
                table: "AspNetUserClaims",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "AspNetUserClaims",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "AspNetUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AspNetUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByBrowserName",
                table: "AspNetUserClaims",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByIp",
                table: "AspNetUserClaims",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "AspNetUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AspNetUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "AspNetUserClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserClaims",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByBrowserName",
                table: "AspNetRoleClaims",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "AspNetRoleClaims",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "AspNetRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "AspNetRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByBrowserName",
                table: "AspNetRoleClaims",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedByIp",
                table: "AspNetRoleClaims",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedByUserId",
                table: "AspNetRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "AspNetRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                table: "AspNetRoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoleClaims",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPhotos",
                table: "UserPhotos",
                column: "alakyId");

            migrationBuilder.CreateTable(
                name: "AppUserUsedPasswords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashedPassword = table.Column<string>(maxLength: 450, nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserUsedPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserUsedPasswords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: true),
                    ModifiedByBrowserName = table.Column<string>(maxLength: 1000, nullable: true),
                    ModifiedByIp = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPhotos_ActivityId",
                table: "UserPhotos",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_UserId1",
                table: "AspNetUserTokens",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId1",
                table: "AspNetUserClaims",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId1",
                table: "AspNetRoleClaims",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserUsedPasswords_UserId",
                table: "AppUserUsedPasswords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_Role_RoleId1",
                table: "AspNetRoleClaims",
                column: "RoleId1",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId1",
                table: "AspNetUserClaims",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Role_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId1",
                table: "AspNetUserTokens",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_UserPhotos_UserPhotoId",
                table: "Comments",
                column: "UserPhotoId",
                principalTable: "UserPhotos",
                principalColumn: "alakyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhotos_Activities_ActivityId",
                table: "UserPhotos",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_Role_RoleId1",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId1",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Role_RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId1",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_UserPhotos_UserPhotoId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPhotos_Activities_ActivityId",
                table: "UserPhotos");

            migrationBuilder.DropTable(
                name: "AppUserUsedPasswords");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPhotos",
                table: "UserPhotos");

            migrationBuilder.DropIndex(
                name: "IX_UserPhotos_ActivityId",
                table: "UserPhotos");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserTokens_UserId1",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_UserId1",
                table: "AspNetUserClaims");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoleClaims_RoleId1",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "alakyId",
                table: "UserPhotos");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "UserPhotos");

            migrationBuilder.DropColumn(
                name: "AccessTokenExpiresDateTime",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "AccessTokenHash",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "CreatedByBrowserName",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedByBrowserName",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedByIp",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiresDateTime",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "RefreshTokenIdHash",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "RefreshTokenIdHashSource",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedByBrowserName",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedByBrowserName",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedByIp",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "CreatedByBrowserName",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedByBrowserName",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedByIp",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "CreatedByBrowserName",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedByBrowserName",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedByIp",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedByUserId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoleClaims");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "UserPhotoId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Activities",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
