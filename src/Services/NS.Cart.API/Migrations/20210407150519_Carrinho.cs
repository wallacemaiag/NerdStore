﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace NS.Cart.API.Migrations
{
    public partial class Carrinho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomersCart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersCart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", nullable: true),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItens_CustomersCart_CartId",
                        column: x => x.CartId,
                        principalTable: "CustomersCart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItens_CartId",
                table: "CartItens",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IDX_Client",
                table: "CustomersCart",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItens");

            migrationBuilder.DropTable(
                name: "CustomersCart");
        }
    }
}
