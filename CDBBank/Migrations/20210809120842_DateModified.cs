using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CDBBank.Migrations
{
    public partial class DateModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterDates");

            migrationBuilder.CreateTable(
                name: "FD",
                columns: table => new
                {
                    FdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FdInvMon = table.Column<double>(type: "float", nullable: false),
                    Month = table.Column<double>(type: "float", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    n = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    BankUsersId = table.Column<int>(type: "int", nullable: true),
                    FdMAmount = table.Column<double>(type: "float", nullable: false),
                    FdInMoney = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FD", x => x.FdId);
                    table.ForeignKey(
                        name: "FK_FD_BankUsers_BankUsersId",
                        column: x => x.BankUsersId,
                        principalTable: "BankUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RD",
                columns: table => new
                {
                    RdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RdInvMon = table.Column<double>(type: "float", nullable: false),
                    Time = table.Column<double>(type: "float", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false),
                    n = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    BankUserId = table.Column<int>(type: "int", nullable: true),
                    FdMatureAmount = table.Column<double>(type: "float", nullable: false),
                    FdInMoney = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RD", x => x.RdId);
                    table.ForeignKey(
                        name: "FK_RD_BankUsers_BankUserId",
                        column: x => x.BankUserId,
                        principalTable: "BankUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FD_BankUsersId",
                table: "FD",
                column: "BankUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_RD_BankUserId",
                table: "RD",
                column: "BankUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FD");

            migrationBuilder.DropTable(
                name: "RD");

            migrationBuilder.CreateTable(
                name: "FilterDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterDates", x => x.Id);
                });
        }
    }
}
