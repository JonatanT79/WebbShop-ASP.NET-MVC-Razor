using Microsoft.EntityFrameworkCore.Migrations;

namespace Product.API.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "InStock", "Maker", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Deskop", 3, "Acer", "PC", 14999f },
                    { 2, "55 Tum", 2, "Philips", "TV", 8999f },
                    { 3, "Iphone", 5, "Apple", "Headphones", 799f },
                    { 4, "Gaming", 4, "Razor", "Keyboard", 1050f }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
