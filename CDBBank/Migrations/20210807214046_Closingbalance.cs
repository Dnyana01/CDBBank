using Microsoft.EntityFrameworkCore.Migrations;

namespace CDBBank.Migrations
{
    public partial class Closingbalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "ClosingBalance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClosingBalance",
                table: "Transactions",
                newName: "Amount");
        }
    }
}
