using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wholesale.server.Migrations
{
    /// <inheritdoc />
    public partial class Fase11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SlpName",
                table: "VisitHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Slpcode",
                table: "VisitHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "U_CodigoPOS",
                table: "VisitHeaders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlpName",
                table: "VisitHeaders");

            migrationBuilder.DropColumn(
                name: "Slpcode",
                table: "VisitHeaders");

            migrationBuilder.DropColumn(
                name: "U_CodigoPOS",
                table: "VisitHeaders");
        }
    }
}
