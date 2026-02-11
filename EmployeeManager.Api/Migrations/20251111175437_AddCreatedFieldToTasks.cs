using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedFieldToTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Tasks",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "NOW()");

            migrationBuilder.AddColumn<int>(
                name: "effortEstimation",
                table: "Tasks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "Tasks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "progress",
                table: "Tasks",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "effortEstimation",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "progress",
                table: "Tasks");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Tasks",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
