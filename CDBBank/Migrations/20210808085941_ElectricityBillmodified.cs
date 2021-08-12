using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CDBBank.Migrations
{
    public partial class ElectricityBillmodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectricityBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitsPrice = table.Column<float>(type: "real", nullable: true),
                    UnitsConsumed = table.Column<int>(type: "int", nullable: false),
                    BillNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneratedBillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Penalty = table.Column<float>(type: "real", nullable: false),
                    BillAmount = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BillUsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricityBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectricityBills_AllUsers_BillUsersId",
                        column: x => x.BillUsersId,
                        principalTable: "AllUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectricityBills_BillUsersId",
                table: "ElectricityBills",
                column: "BillUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectricityBills");
        }
    }
}
