﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BanEndTime",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanEndTime",
                table: "Users");
        }
    }
}
