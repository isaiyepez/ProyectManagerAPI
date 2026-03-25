using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "Status" },
                values: new object[] { new Guid("fc69f73c-acde-4f3c-b9aa-4f9cf4aaad64"), new DateTime(2026, 2, 14, 21, 26, 38, 546, DateTimeKind.Utc).AddTicks(7298), "Initial Project API", "Project Manager v1", 0 });

            migrationBuilder.InsertData(
                table: "TaskItems",
                columns: new[] { "Id", "CreatedAt", "Description", "DueDate", "Priority", "ProjectId", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("18015c01-ba84-41fb-8536-4ad5246f20b8"), new DateTime(2026, 2, 14, 21, 26, 38, 546, DateTimeKind.Utc).AddTicks(7472), "Generate README of the Project", new DateTime(2026, 3, 1, 21, 26, 38, 546, DateTimeKind.Utc).AddTicks(7471), 0, new Guid("fc69f73c-acde-4f3c-b9aa-4f9cf4aaad64"), 0, "Tech documentation" },
                    { new Guid("26edf2f3-0cda-4e9c-97e1-e55ae1da4f3f"), new DateTime(2026, 2, 14, 21, 26, 38, 546, DateTimeKind.Utc).AddTicks(7468), "Implementation of Repository Pattern for the Project", new DateTime(2026, 2, 24, 21, 26, 38, 546, DateTimeKind.Utc).AddTicks(7467), 1, new Guid("fc69f73c-acde-4f3c-b9aa-4f9cf4aaad64"), 1, "Generate Repo" },
                    { new Guid("829698f6-d9c4-454e-8baf-8f5dab9a788d"), new DateTime(2026, 2, 14, 21, 26, 38, 546, DateTimeKind.Utc).AddTicks(7462), "Install packages and run migrations", new DateTime(2026, 2, 21, 21, 26, 38, 546, DateTimeKind.Utc).AddTicks(7456), 2, new Guid("fc69f73c-acde-4f3c-b9aa-4f9cf4aaad64"), 2, "Configure database" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: new Guid("18015c01-ba84-41fb-8536-4ad5246f20b8"));

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: new Guid("26edf2f3-0cda-4e9c-97e1-e55ae1da4f3f"));

            migrationBuilder.DeleteData(
                table: "TaskItems",
                keyColumn: "Id",
                keyValue: new Guid("829698f6-d9c4-454e-8baf-8f5dab9a788d"));

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: new Guid("fc69f73c-acde-4f3c-b9aa-4f9cf4aaad64"));
        }
    }
}
