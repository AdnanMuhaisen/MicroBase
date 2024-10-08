﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroBase.CommandService.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Platforms",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ExternalId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Platforms", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Commands",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Activity = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                CommandLine = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                PlatformId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Commands", x => x.Id);
                table.ForeignKey(
                    name: "FK_Commands_Platforms_PlatformId",
                    column: x => x.PlatformId,
                    principalTable: "Platforms",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Commands_PlatformId",
            table: "Commands",
            column: "PlatformId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Commands");

        migrationBuilder.DropTable(
            name: "Platforms");
    }
}