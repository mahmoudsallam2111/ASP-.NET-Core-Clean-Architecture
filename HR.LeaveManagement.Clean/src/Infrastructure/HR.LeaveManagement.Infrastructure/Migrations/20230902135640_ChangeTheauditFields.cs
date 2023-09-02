using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTheauditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "leaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "leaveTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "leaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "leaveRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "leaveRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "leaveRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "leaveAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "leaveAllocations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "DateCreated", "DateModified", "ModifiedBy" },
                values: new object[] { null, new DateTime(2023, 9, 2, 16, 56, 40, 350, DateTimeKind.Local).AddTicks(5137), new DateTime(2023, 9, 2, 16, 56, 40, 350, DateTimeKind.Local).AddTicks(5192), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "leaveTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "leaveTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "leaveRequests");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "leaveAllocations");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "leaveAllocations");

            migrationBuilder.UpdateData(
                table: "leaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 8, 28, 21, 31, 17, 386, DateTimeKind.Local).AddTicks(34), new DateTime(2023, 8, 28, 21, 31, 17, 386, DateTimeKind.Local).AddTicks(84) });
        }
    }
}
