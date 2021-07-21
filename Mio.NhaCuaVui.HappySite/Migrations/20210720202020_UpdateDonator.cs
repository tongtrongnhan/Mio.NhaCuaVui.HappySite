using Microsoft.EntityFrameworkCore.Migrations;

namespace Mio.NhaCuaVui.HappySite.Migrations
{
    public partial class UpdateDonator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "DonatorOrganizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NeedId",
                table: "DonatorOrganizations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "DonatorOrganizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganizationName",
                table: "DonatorOrganizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProposetorEmail",
                table: "DonatorOrganizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProposetorName",
                table: "DonatorOrganizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProposetorPhone",
                table: "DonatorOrganizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "DonatorOrganizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WardId",
                table: "DonatorOrganizations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DonatorOrganizations_NeedId",
                table: "DonatorOrganizations",
                column: "NeedId");

            migrationBuilder.CreateIndex(
                name: "IX_DonatorOrganizations_WardId",
                table: "DonatorOrganizations",
                column: "WardId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonatorOrganizations_Needs_NeedId",
                table: "DonatorOrganizations",
                column: "NeedId",
                principalTable: "Needs",
                principalColumn: "NeedId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DonatorOrganizations_Wards_WardId",
                table: "DonatorOrganizations",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "WardId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonatorOrganizations_Needs_NeedId",
                table: "DonatorOrganizations");

            migrationBuilder.DropForeignKey(
                name: "FK_DonatorOrganizations_Wards_WardId",
                table: "DonatorOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_DonatorOrganizations_NeedId",
                table: "DonatorOrganizations");

            migrationBuilder.DropIndex(
                name: "IX_DonatorOrganizations_WardId",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "NeedId",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "OrganizationName",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "ProposetorEmail",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "ProposetorName",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "ProposetorPhone",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "DonatorOrganizations");
        }
    }
}
