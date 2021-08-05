using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mio.NhaCuaVui.HappySite.Migrations
{
    public partial class AddShipperRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShipperRequests",
                columns: table => new
                {
                    ShipperRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipperName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    ShipperPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AvailableDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableFromHour = table.Column<int>(type: "int", nullable: false),
                    AvailableToHour = table.Column<int>(type: "int", nullable: false),
                    WardId = table.Column<int>(type: "int", nullable: false),
                    Transportation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsValidated = table.Column<bool>(type: "bit", nullable: false),
                    ValidatedUserId = table.Column<int>(type: "int", nullable: true),
                    ValidateMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipperRequests", x => x.ShipperRequestId);
                    table.ForeignKey(
                        name: "FK_ShipperRequests_Users_ValidatedUserId",
                        column: x => x.ValidatedUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShipperRequests_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "WardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipperRequests_ValidatedUserId",
                table: "ShipperRequests",
                column: "ValidatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperRequests_WardId",
                table: "ShipperRequests",
                column: "WardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipperRequests");
        }
    }
}
