using Microsoft.EntityFrameworkCore.Migrations;

namespace Mio.NhaCuaVui.HappySite.Migrations
{
    public partial class builder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeneficiaryTypes",
                columns: table => new
                {
                    BeneficiaryTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeneficiaryTypes", x => x.BeneficiaryTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiaries",
                columns: table => new
                {
                    BeneficiaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BeneficiaryTypeId = table.Column<int>(type: "int", nullable: false),
                    ProposetorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProposetorPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProposetorEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeedId = table.Column<int>(type: "int", nullable: true),
                    WardId = table.Column<int>(type: "int", nullable: true),
                    OrganizationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => x.BeneficiaryId);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_BeneficiaryTypes_BeneficiaryTypeId",
                        column: x => x.BeneficiaryTypeId,
                        principalTable: "BeneficiaryTypes",
                        principalColumn: "BeneficiaryTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Needs_NeedId",
                        column: x => x.NeedId,
                        principalTable: "Needs",
                        principalColumn: "NeedId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "WardId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BenificaryCategoryQuantity",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BenificaryId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenificaryCategoryQuantity", x => new { x.CategoryId, x.BenificaryId });
                    table.ForeignKey(
                        name: "FK_BenificaryCategoryQuantity_Beneficiaries_BenificaryId",
                        column: x => x.BenificaryId,
                        principalTable: "Beneficiaries",
                        principalColumn: "BeneficiaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BenificaryCategoryQuantity_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_BeneficiaryTypeId",
                table: "Beneficiaries",
                column: "BeneficiaryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_NeedId",
                table: "Beneficiaries",
                column: "NeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_WardId",
                table: "Beneficiaries",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_BenificaryCategoryQuantity_BenificaryId",
                table: "BenificaryCategoryQuantity",
                column: "BenificaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenificaryCategoryQuantity");

            migrationBuilder.DropTable(
                name: "Beneficiaries");

            migrationBuilder.DropTable(
                name: "BeneficiaryTypes");
        }
    }
}
