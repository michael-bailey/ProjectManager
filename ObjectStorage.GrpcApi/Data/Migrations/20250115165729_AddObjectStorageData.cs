﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObjectStorage.GrpcApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddObjectStorageData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BinaryObjectDataSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinaryObjectDataSet", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinaryObjectDataSet");
        }
    }
}
