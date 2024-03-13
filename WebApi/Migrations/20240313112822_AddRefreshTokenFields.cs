using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "40631648-71eb-4df9-be70-0529b24c2198", null, "User", "USER" },
                    { "4dd28d87-ebfa-46de-8340-41324b889363", null, "Admin", "ADMIN" },
                    { "8839b595-7691-4fc8-b7eb-e232d41250c0", null, "Editor", "EDITOR" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdate" },
                values: new object[] { new DateTime(2024, 3, 13, 14, 28, 22, 174, DateTimeKind.Local).AddTicks(197), new DateTime(2024, 3, 13, 14, 28, 22, 174, DateTimeKind.Local).AddTicks(212) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastUpdate" },
                values: new object[] { new DateTime(2024, 3, 13, 14, 28, 22, 174, DateTimeKind.Local).AddTicks(214), new DateTime(2024, 3, 13, 14, 28, 22, 174, DateTimeKind.Local).AddTicks(214) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastUpdate" },
                values: new object[] { new DateTime(2024, 3, 13, 14, 28, 22, 174, DateTimeKind.Local).AddTicks(216), new DateTime(2024, 3, 13, 14, 28, 22, 174, DateTimeKind.Local).AddTicks(216) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40631648-71eb-4df9-be70-0529b24c2198");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4dd28d87-ebfa-46de-8340-41324b889363");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8839b595-7691-4fc8-b7eb-e232d41250c0");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

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
    }
}
