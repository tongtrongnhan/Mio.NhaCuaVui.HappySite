using Microsoft.EntityFrameworkCore.Migrations;

namespace Mio.NhaCuaVui.HappySite.Migrations
{
    public partial class Udpate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HadTransportation",
                table: "DonatorOrganizations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HadTransportation",
                table: "Beneficiaries",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HadTransportation",
                table: "DonatorOrganizations");

            migrationBuilder.DropColumn(
                name: "HadTransportation",
                table: "Beneficiaries");
        }
    }
}
