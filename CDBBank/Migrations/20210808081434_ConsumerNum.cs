using Microsoft.EntityFrameworkCore.Migrations;

namespace CDBBank.Migrations
{
    public partial class ConsumerNum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ElectricityConsumerNum",
                table: "AllUsers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElectricityConsumerNum",
                table: "AllUsers");
        }
    }
}
