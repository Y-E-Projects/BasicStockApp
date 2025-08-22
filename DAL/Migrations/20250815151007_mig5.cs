using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinimumQuantity",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SupplierKey",
                table: "Products",
                type: "char(36)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReturnHistories",
                columns: table => new
                {
                    Key = table.Column<Guid>(type: "char(36)", nullable: false),
                    SellItemKey = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProductKey = table.Column<Guid>(type: "char(36)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "longtext", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnHistories", x => x.Key);
                    table.ForeignKey(
                        name: "FK_ReturnHistories_Products_ProductKey",
                        column: x => x.ProductKey,
                        principalTable: "Products",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReturnHistories_SellItems_SellItemKey",
                        column: x => x.SellItemKey,
                        principalTable: "SellItems",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Key = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    ContactName = table.Column<string>(type: "longtext", nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Address = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Key);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierKey",
                table: "Products",
                column: "SupplierKey");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnHistories_ProductKey",
                table: "ReturnHistories",
                column: "ProductKey");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnHistories_SellItemKey",
                table: "ReturnHistories",
                column: "SellItemKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_SupplierKey",
                table: "Products",
                column: "SupplierKey",
                principalTable: "Suppliers",
                principalColumn: "Key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_SupplierKey",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ReturnHistories");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Products_SupplierKey",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "MinimumQuantity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SupplierKey",
                table: "Products");
        }
    }
}
