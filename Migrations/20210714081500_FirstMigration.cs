using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModelProject.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ModelUsers",
                columns: table => new
                {
                    ModelUserId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    DOF = table.Column<DateTime>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    MobilePhone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    AgencyUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelUsers", x => x.ModelUserId);
                    table.ForeignKey(
                        name: "FK_ModelUsers_ClientUsers_AgencyUserId",
                        column: x => x.AgencyUserId,
                        principalTable: "ClientUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    AppId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AgeCat = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    Instagram = table.Column<string>(nullable: false),
                    LinkedIn = table.Column<string>(nullable: false),
                    ModelUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.AppId);
                    table.ForeignKey(
                        name: "FK_Apps_ModelUsers_ModelUserId",
                        column: x => x.ModelUserId,
                        principalTable: "ModelUsers",
                        principalColumn: "ModelUserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apps_ModelUserId",
                table: "Apps",
                column: "ModelUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelUsers_AgencyUserId",
                table: "ModelUsers",
                column: "AgencyUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apps");

            migrationBuilder.DropTable(
                name: "ModelUsers");

            migrationBuilder.DropTable(
                name: "ClientUsers");
        }
    }
}
