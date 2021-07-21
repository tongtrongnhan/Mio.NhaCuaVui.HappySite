using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mio.NhaCuaVui.HappySite.Migrations
{
    public partial class UpdateValidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DonatorOrganizations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsValidated",
                table: "DonatorOrganizations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidatedAt",
                table: "DonatorOrganizations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ValidatedUserId",
                table: "DonatorOrganizations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Beneficiaries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsValidated",
                table: "Beneficiaries",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidatedAt",
                table: "Beneficiaries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ValidatedUserId",
                table: "Beneficiaries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonatorOrganizations_ValidatedUserId",
                table: "DonatorOrganizations",
                column: "ValidatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_ValidatedUserId",
                table: "Beneficiaries",
                column: "ValidatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiaries_Users_ValidatedUserId",
                table: "Beneficiaries",
                column: "ValidatedUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DonatorOrganizations_Users_ValidatedUserId",
                table: "DonatorOrganizations",
                column: "ValidatedUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiaries_Users_ValidatedUserId",
                table: "Beneficiaries");

            migrationBuilder.DropForeignKey(
                name: "FK_DonatorOrganizations_Users_ValidatedUserId",
                table: "DonatorOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_DonatorOrganizations_ValidatedUserId",
                table: "DonatorOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_Beneficiaries_ValidatedUserId",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "IsValidated",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "ValidatedAt",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "ValidatedUserId",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "IsValidated",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "ValidatedAt",
                table: "Beneficiaries");

            migrationBuilder.DropColumn(
                name: "ValidatedUserId",
                table: "Beneficiaries");
        }
    }
}
