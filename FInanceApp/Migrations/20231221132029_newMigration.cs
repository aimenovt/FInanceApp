﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FInanceApp.Migrations
{
    /// <inheritdoc />
    public partial class newMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "Countries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "test",
                table: "Countries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}