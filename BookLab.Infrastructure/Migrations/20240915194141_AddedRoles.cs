using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Role",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 15, 22, 41, 41, 184, DateTimeKind.Local).AddTicks(6049), null, "Customer", new DateTime(2024, 9, 15, 22, 41, 41, 184, DateTimeKind.Local).AddTicks(6058), null },
                    { 2, new DateTime(2024, 9, 15, 22, 41, 41, 184, DateTimeKind.Local).AddTicks(6060), null, "Admin", new DateTime(2024, 9, 15, 22, 41, 41, 184, DateTimeKind.Local).AddTicks(6060), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Role",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
