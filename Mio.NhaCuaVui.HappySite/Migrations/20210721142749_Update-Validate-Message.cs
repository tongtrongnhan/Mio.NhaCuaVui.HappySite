using Microsoft.EntityFrameworkCore.Migrations;

namespace Mio.NhaCuaVui.HappySite.Migrations
{
    public partial class UpdateValidateMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NotValidateMessage",
                table: "DonatorOrganizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotValidateMessage",
                table: "Beneficiaries",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotValidateMessage",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "NotValidateMessage",
                table: "Beneficiaries");
        }
    }
}
