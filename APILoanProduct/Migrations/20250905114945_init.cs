using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APILoanProduct.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanProducts",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanProducts", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "AccountSettings",
                columns: table => new
                {
                    settingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinimumAgeRequirement = table.Column<int>(type: "int", nullable: false),
                    MaximumAgeRequirement = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSettings", x => x.settingId);
                    table.ForeignKey(
                        name: "FK_AccountSettings_LoanProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "LoanProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interestRates",
                columns: table => new
                {
                    InterestId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Interesttype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    interestRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProcessingFee = table.Column<double>(type: "float", nullable: false),
                    Penaltyrate = table.Column<double>(type: "float", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interestRates", x => x.InterestId);
                    table.ForeignKey(
                        name: "FK_interestRates_LoanProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "LoanProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanAmountDetails",
                columns: table => new
                {
                    LoanAmountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    minAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    maxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DefaultAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tranches = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanAmountDetails", x => x.LoanAmountId);
                    table.ForeignKey(
                        name: "FK_LoanAmountDetails_LoanProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "LoanProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanDocuments",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressProof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncomeProoof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityProof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalDocuments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDocuments", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_LoanDocuments_LoanProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "LoanProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdAvailability",
                columns: table => new
                {
                    AvailabilityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductAvailabilityTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdAvailability", x => x.AvailabilityId);
                    table.ForeignKey(
                        name: "FK_ProdAvailability_LoanProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "LoanProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepaymentDetails",
                columns: table => new
                {
                    RepaymentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinimumTenure = table.Column<int>(type: "int", nullable: false),
                    MaximumTenure = table.Column<int>(type: "int", nullable: false),
                    GracePeriodDays = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepaymentDetails", x => x.RepaymentId);
                    table.ForeignKey(
                        name: "FK_RepaymentDetails_LoanProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "LoanProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountSettings_ProductId",
                table: "AccountSettings",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_interestRates_ProductId",
                table: "interestRates",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanAmountDetails_ProductId",
                table: "LoanAmountDetails",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoanDocuments_ProductId",
                table: "LoanDocuments",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProdAvailability_ProductId",
                table: "ProdAvailability",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepaymentDetails_ProductId",
                table: "RepaymentDetails",
                column: "ProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountSettings");

            migrationBuilder.DropTable(
                name: "interestRates");

            migrationBuilder.DropTable(
                name: "LoanAmountDetails");

            migrationBuilder.DropTable(
                name: "LoanDocuments");

            migrationBuilder.DropTable(
                name: "ProdAvailability");

            migrationBuilder.DropTable(
                name: "RepaymentDetails");

            migrationBuilder.DropTable(
                name: "LoanProducts");
        }
    }
}
