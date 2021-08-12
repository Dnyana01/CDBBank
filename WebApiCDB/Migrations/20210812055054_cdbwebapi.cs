using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiCDB.Migrations
{
    public partial class cdbwebapi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<long>(type: "bigint", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<double>(type: "float", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    PinCode = table.Column<int>(type: "int", nullable: false),
                    Sques = table.Column<int>(type: "int", nullable: false),
                    SAns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoB = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankUser", x => x.Id);
                });

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
                        name: "FK_FD_BankUser_BankUsersId",
                        column: x => x.BankUsersId,
                        principalTable: "BankUser",
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
                        name: "FK_RD_BankUser_BankUserId",
                        column: x => x.BankUserId,
                        principalTable: "BankUser",
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

            migrationBuilder.DropTable(
                name: "BankUser");
        }
    }
}
