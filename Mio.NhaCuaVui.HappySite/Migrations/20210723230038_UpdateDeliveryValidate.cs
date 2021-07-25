using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mio.NhaCuaVui.HappySite.Migrations
{
    public partial class UpdateDeliveryValidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredAt",
                table: "Deliveries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelivery",
                table: "Deliveries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsValidated",
                table: "Deliveries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ValidatedByUserId",
                table: "Deliveries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidateddAt",
                table: "Deliveries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ValidatedByUserId",
                table: "Deliveries",
                column: "ValidatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Users_ValidatedByUserId",
                table: "Deliveries",
                column: "ValidatedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Users_ValidatedByUserId",
                table: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_ValidatedByUserId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "DeliveredAt",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "IsDelivery",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "IsValidated",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "ValidatedByUserId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "ValidateddAt",
                table: "Deliveries");
        }
    }
}
