using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_ASP.net.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "receipt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type_receipt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    receipt_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    observations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date_emision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount_total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receipt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "receipt_detail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    receipt_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receipt_detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_receipt_detail_receipt_receipt_id",
                        column: x => x.receipt_id,
                        principalTable: "receipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_receipt_detail_receipt_id",
                table: "receipt_detail",
                column: "receipt_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "receipt_detail");

            migrationBuilder.DropTable(
                name: "receipt");
        }
    }
}
