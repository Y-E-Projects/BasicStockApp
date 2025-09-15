using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class migAddEntityIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_StockHistories_ProductKey",
                table: "StockHistories",
                newName: "IX_StockHistory_ProductKey");

            migrationBuilder.RenameIndex(
                name: "IX_SellItems_SellKey",
                table: "SellItems",
                newName: "IX_SellItem_SellKey");

            migrationBuilder.RenameIndex(
                name: "IX_SellItems_ProductKey",
                table: "SellItems",
                newName: "IX_SellItem_ProductKey");

            migrationBuilder.RenameIndex(
                name: "IX_ReturnHistories_SellItemKey",
                table: "ReturnHistories",
                newName: "IX_ReturnHistory_SellItemKey");

            migrationBuilder.RenameIndex(
                name: "IX_ReturnHistories_ProductKey",
                table: "ReturnHistories",
                newName: "IX_ReturnHistory_ProductKey");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SupplierKey",
                table: "Products",
                newName: "IX_Product_SupplierKey");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryKey",
                table: "Products",
                newName: "IX_Product_CategoryKey");

            migrationBuilder.RenameIndex(
                name: "IX_PriceHistories_ProductKey",
                table: "PriceHistories",
                newName: "IX_PriceHistory_ProductKey");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Suppliers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Suppliers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "SellCode",
                table: "Sells",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Products",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Email",
                table: "Suppliers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Name",
                table: "Suppliers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Sell_SellCode",
                table: "Sells",
                column: "SellCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Barcode",
                table: "Products",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                table: "Categories",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Supplier_Email",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Supplier_Name",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Sell_SellCode",
                table: "Sells");

            migrationBuilder.DropIndex(
                name: "IX_Product_Barcode",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Product_Name",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Category_Name",
                table: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_StockHistory_ProductKey",
                table: "StockHistories",
                newName: "IX_StockHistories_ProductKey");

            migrationBuilder.RenameIndex(
                name: "IX_SellItem_SellKey",
                table: "SellItems",
                newName: "IX_SellItems_SellKey");

            migrationBuilder.RenameIndex(
                name: "IX_SellItem_ProductKey",
                table: "SellItems",
                newName: "IX_SellItems_ProductKey");

            migrationBuilder.RenameIndex(
                name: "IX_ReturnHistory_SellItemKey",
                table: "ReturnHistories",
                newName: "IX_ReturnHistories_SellItemKey");

            migrationBuilder.RenameIndex(
                name: "IX_ReturnHistory_ProductKey",
                table: "ReturnHistories",
                newName: "IX_ReturnHistories_ProductKey");

            migrationBuilder.RenameIndex(
                name: "IX_Product_SupplierKey",
                table: "Products",
                newName: "IX_Products_SupplierKey");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CategoryKey",
                table: "Products",
                newName: "IX_Products_CategoryKey");

            migrationBuilder.RenameIndex(
                name: "IX_PriceHistory_ProductKey",
                table: "PriceHistories",
                newName: "IX_PriceHistories_ProductKey");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Suppliers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Suppliers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "SellCode",
                table: "Sells",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Barcode",
                table: "Products",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");
        }
    }
}
