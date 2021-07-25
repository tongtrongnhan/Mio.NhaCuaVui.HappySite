using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mio.NhaCuaVui.HappySite.Migrations
{
    public partial class UpdateDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyDonatorOrganizationId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    DeliveryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonatorOrganizationId = table.Column<int>(type: "int", nullable: false),
                    BeneficiaryId = table.Column<int>(type: "int", nullable: true),
                    BeneficiaryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCreateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.DeliveryId);
                    table.ForeignKey(
                        name: "FK_Deliveries_Beneficiaries_BeneficiaryId",
                        column: x => x.BeneficiaryId,
                        principalTable: "Beneficiaries",
                        principalColumn: "BeneficiaryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Deliveries_DonatorOrganizations_DonatorOrganizationId",
                        column: x => x.DonatorOrganizationId,
                        principalTable: "DonatorOrganizations",
                        principalColumn: "DonatorOrganizationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deliveries_Users_UserCreateId",
                        column: x => x.UserCreateId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    DeliveryId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryCategory", x => new { x.CategoryId, x.DeliveryId });
                    table.ForeignKey(
                        name: "FK_DeliveryCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryCategory_Deliveries_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Deliveries",
                        principalColumn: "DeliveryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_MyDonatorOrganizationId",
                table: "Users",
                column: "MyDonatorOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_BeneficiaryId",
                table: "Deliveries",
                column: "BeneficiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DonatorOrganizationId",
                table: "Deliveries",
                column: "DonatorOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_UserCreateId",
                table: "Deliveries",
                column: "UserCreateId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryCategory_DeliveryId",
                table: "DeliveryCategory",
                column: "DeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_DonatorOrganizations_MyDonatorOrganizationId",
                table: "Users",
                column: "MyDonatorOrganizationId",
                principalTable: "DonatorOrganizations",
                principalColumn: "DonatorOrganizationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_DonatorOrganizations_MyDonatorOrganizationId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "DeliveryCategory");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropIndex(
                name: "IX_Users_MyDonatorOrganizationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MyDonatorOrganizationId",
                table: "Users");
        }
    }
}
