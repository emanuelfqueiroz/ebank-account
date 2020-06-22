using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraStructure.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Agency_AccountNumber",
                table: "Accounts",
                columns: new[] { "Agency", "AccountNumber" },
                unique: true,
                filter: "[Agency] IS NOT NULL AND [AccountNumber] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_Agency_AccountNumber",
                table: "Accounts");
        }
    }
}
