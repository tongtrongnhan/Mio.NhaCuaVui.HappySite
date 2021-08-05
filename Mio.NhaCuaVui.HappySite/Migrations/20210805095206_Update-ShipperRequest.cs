using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mio.NhaCuaVui.HappySite.Migrations
{
    public partial class UpdateShipperRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipperRequests_Wards_WardId",
                table: "ShipperRequests");

            migrationBuilder.RenameColumn(
                name: "WardId",
                table: "ShipperRequests",
                newName: "DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_ShipperRequests_WardId",
                table: "ShipperRequests",
                newName: "IX_ShipperRequests_DistrictId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidatedAt",
                table: "ShipperRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_ShipperRequests_Districts_DistrictId",
                table: "ShipperRequests",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "DistrictId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipperRequests_Districts_DistrictId",
                table: "ShipperRequests");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "ShipperRequests",
                newName: "WardId");

            migrationBuilder.RenameIndex(
                name: "IX_ShipperRequests_DistrictId",
                table: "ShipperRequests",
                newName: "IX_ShipperRequests_WardId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ValidatedAt",
                table: "ShipperRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipperRequests_Wards_WardId",
                table: "ShipperRequests",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "WardId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
