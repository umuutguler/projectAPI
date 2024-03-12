using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "Yazılım Geliştirme" },
                    { 2, "Test ve Kalite Güvence" },
                    { 3, "Proje Yönetimi" },
                    { 4, "Ürün Yönetimi" },
                    { 5, "Satış ve Pazarlama" },
                    { 6, "İnsan Kaynakları" },
                    { 7, "Finans ve Muhasebe" },
                    { 8, "Bilgi Teknolojileri (BT)" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "LastUpdate", "Price", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 12, 23, 9, 4, 35, DateTimeKind.Local).AddTicks(8507), "Description", new DateTime(2024, 3, 12, 23, 9, 4, 35, DateTimeKind.Local).AddTicks(8524), 100m, "Product 1" },
                    { 2, new DateTime(2024, 3, 12, 23, 9, 4, 35, DateTimeKind.Local).AddTicks(8525), "Description", new DateTime(2024, 3, 12, 23, 9, 4, 35, DateTimeKind.Local).AddTicks(8526), 75m, "Product 2" },
                    { 3, new DateTime(2024, 3, 12, 23, 9, 4, 35, DateTimeKind.Local).AddTicks(8527), "Description", new DateTime(2024, 3, 12, 23, 9, 4, 35, DateTimeKind.Local).AddTicks(8528), 200m, "Product 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
