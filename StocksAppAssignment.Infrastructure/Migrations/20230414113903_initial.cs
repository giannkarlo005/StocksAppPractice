using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StocksAppAssignment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SellOrders",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StockSymbol = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OrderQuantity = table.Column<double>(type: "float", nullable: false),
                    OrderPrice = table.Column<double>(type: "float", nullable: false),
                    DateAndTimeOfOrder = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellOrders", x => x.OrderID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellOrders");
        }
    }
}
