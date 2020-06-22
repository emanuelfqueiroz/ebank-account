using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfraStructure.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Balance = table.Column<decimal>(type: "Money", nullable: false),
                    Agency = table.Column<string>(maxLength: 20, nullable: true),
                    AccountNumber = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trasnfers",
                columns: table => new
                {
                    EntryId = table.Column<Guid>(nullable: false),
                    DepositorId = table.Column<Guid>(nullable: false),
                    BeneficiaryId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trasnfers", x => x.EntryId);
                    table.ForeignKey(
                        name: "FK_Trasnfers_Accounts_BeneficiaryId",
                        column: x => x.BeneficiaryId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trasnfers_Accounts_DepositorId",
                        column: x => x.DepositorId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trasnfers_BeneficiaryId",
                table: "Trasnfers",
                column: "BeneficiaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Trasnfers_DepositorId",
                table: "Trasnfers",
                column: "DepositorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trasnfers");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
