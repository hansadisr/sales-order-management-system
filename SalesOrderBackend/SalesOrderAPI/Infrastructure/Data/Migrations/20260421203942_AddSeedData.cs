using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SalesOrderAPI.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "ClientId", "Address1", "Address2", "Address3", "Name", "PostCode", "State", "Suburb" },
                values: new object[,]
                {
                    { 1, "123 Business Street", "Suite 100", "", "Acme Corporation", "2000", "NSW", "Sydney" },
                    { 2, "456 Commerce Ave", "Level 5", "", "Global Industries", "3000", "VIC", "Melbourne" },
                    { 3, "789 Innovation Drive", "", "", "Tech Solutions Pty Ltd", "4000", "QLD", "Brisbane" }
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "Description", "ItemCode", "Price", "TaxRate" },
                values: new object[,]
                {
                    { 1, "Professional Consulting Services", "ITEM001", 150.00m, 0.10m },
                    { 2, "Software License (Annual)", "ITEM002", 1200.00m, 0.10m },
                    { 3, "Technical Support Package", "ITEM003", 500.00m, 0.10m },
                    { 4, "Hardware Installation", "ITEM004", 350.00m, 0.10m },
                    { 5, "Training and Documentation", "ITEM005", 250.00m, 0.10m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "ClientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "ClientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Client",
                keyColumn: "ClientId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "ItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "ItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "ItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "ItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "ItemId",
                keyValue: 5);
        }
    }
}
