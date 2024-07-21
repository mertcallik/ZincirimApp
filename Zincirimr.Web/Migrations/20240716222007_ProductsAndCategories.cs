using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Zincirimr.Web.Migrations
{
    /// <inheritdoc />
    public partial class ProductsAndCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => new { x.CategoryId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Telefon", "telefon" },
                    { 2, "Tablet", "tablet" },
                    { 3, "Bilgisayar", "bilgisayar" },
                    { 4, "Laptop", "laptop" },
                    { 5, "Akıllı Ev Aletleri", "akillievaletleri" },
                    { 6, "Eletronik", "Elektronik" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Image", "Name", "Price", "Url" },
                values: new object[,]
                {
                    { 1, "iphone1.png", "iPhone 15 Plus", 75000.0, "iphone15plus" },
                    { 2, "iphone1.png", "iPhone 15 Pro Max", 95000.0, "iphone15promax" },
                    { 3, "iphone1.png", "iPhone 15 Pro", 95000.0, "iphone15pro" },
                    { 4, "iphone1.png", "iPhone 15", 95000.0, "iphone15" },
                    { 5, "iphone1.png", "iPhone 14 Plus", 95000.0, "iphone14plus" },
                    { 6, "iphone1.png", "iPhone 14 Pro Max", 95000.0, "iphone14promax" },
                    { 7, "iphone1.png", "iPhone 14 Pro", 95000.0, "iphone14pro" },
                    { 8, "iphone1.png", "iPhone 14", 95000.0, "iphone14" },
                    { 9, "iphone1.png", "iPhone 13 Pro Max", 95000.0, "iphone13promax" },
                    { 10, "macbook.png", "MacBook Air M2", 95000.0, "macbookairm2" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 1, 8 },
                    { 1, 9 },
                    { 3, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_Url",
                table: "Category",
                column: "Url",
                unique: true,
                filter: "[Url] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Url",
                table: "Product",
                column: "Url",
                unique: true,
                filter: "[Url] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductId",
                table: "ProductCategory",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
