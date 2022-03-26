using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Payment.Infrastructure.Migrations
{
    public partial class changeddatatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedOn",
                schema: "Transaction",
                table: "tblTransactions",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                schema: "Transaction",
                table: "tblTransactions",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedOn",
                schema: "Transaction",
                table: "tblTransactions",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                schema: "Transaction",
                table: "tblTransactions",
                type: "smalldatetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
