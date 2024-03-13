using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37b9db8f-a002-4537-bc05-c0766513f50b", null, "Editor", "EDITOR" },
                    { "75792d66-a766-4553-8957-fd6698703d5b", null, "Admin", "ADMIN" },
                    { "c264e250-5508-488d-aefb-c507c98a350a", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdate" },
                values: new object[] { new DateTime(2024, 3, 13, 0, 45, 40, 909, DateTimeKind.Local).AddTicks(7074), new DateTime(2024, 3, 13, 0, 45, 40, 909, DateTimeKind.Local).AddTicks(7088) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastUpdate" },
                values: new object[] { new DateTime(2024, 3, 13, 0, 45, 40, 909, DateTimeKind.Local).AddTicks(7090), new DateTime(2024, 3, 13, 0, 45, 40, 909, DateTimeKind.Local).AddTicks(7090) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastUpdate" },
                values: new object[] { new DateTime(2024, 3, 13, 0, 45, 40, 909, DateTimeKind.Local).AddTicks(7091), new DateTime(2024, 3, 13, 0, 45, 40, 909, DateTimeKind.Local).AddTicks(7092) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37b9db8f-a002-4537-bc05-c0766513f50b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75792d66-a766-4553-8957-fd6698703d5b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c264e250-5508-488d-aefb-c507c98a350a");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdate" },
                values: new object[] { new DateTime(2024, 3, 13, 0, 18, 20, 24, DateTimeKind.Local).AddTicks(4173), new DateTime(2024, 3, 13, 0, 18, 20, 24, DateTimeKind.Local).AddTicks(4187) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastUpdate" },
                values: new object[] { new DateTime(2024, 3, 13, 0, 18, 20, 24, DateTimeKind.Local).AddTicks(4189), new DateTime(2024, 3, 13, 0, 18, 20, 24, DateTimeKind.Local).AddTicks(4189) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastUpdate" },
                values: new object[] { new DateTime(2024, 3, 13, 0, 18, 20, 24, DateTimeKind.Local).AddTicks(4191), new DateTime(2024, 3, 13, 0, 18, 20, 24, DateTimeKind.Local).AddTicks(4191) });
        }
    }
}
