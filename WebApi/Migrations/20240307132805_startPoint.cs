﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class startPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "LastUpdate", "Price", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 7, 16, 28, 5, 344, DateTimeKind.Local).AddTicks(6602), "Description", new DateTime(2024, 3, 7, 16, 28, 5, 344, DateTimeKind.Local).AddTicks(6614), 100m, "Product 1" },
                    { 2, new DateTime(2024, 3, 7, 16, 28, 5, 344, DateTimeKind.Local).AddTicks(6615), "Description", new DateTime(2024, 3, 7, 16, 28, 5, 344, DateTimeKind.Local).AddTicks(6616), 75m, "Product 2" },
                    { 3, new DateTime(2024, 3, 7, 16, 28, 5, 344, DateTimeKind.Local).AddTicks(6617), "Description", new DateTime(2024, 3, 7, 16, 28, 5, 344, DateTimeKind.Local).AddTicks(6617), 200m, "Product 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}