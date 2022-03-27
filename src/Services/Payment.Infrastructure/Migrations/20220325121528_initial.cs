using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Transaction");

            migrationBuilder.CreateTable(
                name: "tblTransactions",
                schema: "Transaction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    CardholderNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    HolderName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ExpirationMonth = table.Column<byte>(type: "tinyint", nullable: false),
                    ExpirationYear = table.Column<byte>(type: "tinyint", nullable: false),
                    CVV = table.Column<int>(type: "int", nullable: false),
                    OrderReference = table.Column<string>(nullable: false),
                    TransactionStatus = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTransactions", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblTransactions",
                schema: "Transaction");
        }
    }
}
