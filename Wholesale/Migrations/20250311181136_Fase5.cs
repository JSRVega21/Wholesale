using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wholesale.server.Migrations
{
    /// <inheritdoc />
    public partial class Fase5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "TotalVisits",
                table: "VisitHeaders",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "VisitHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Routes",
                table: "VisitHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeVisit",
                table: "VisitDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RegionHeaders",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordLog_CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "Usuario que creo el registro"),
                    RecordLog_CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha y hora de creación del registro"),
                    RecordLog_UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "Ultimo usuario que modificó el registro"),
                    RecordLog_UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ultima fecha y hora de actualización del registro"),
                    RecordLog_IsActive = table.Column<bool>(type: "bit", nullable: true, comment: "Registro activo"),
                    RecordLog_IsSystem = table.Column<bool>(type: "bit", nullable: true, comment: "Es un registro del sistema, los registros del sistema no pueden ser eliminados"),
                    RecordLog_SyncStatus = table.Column<int>(type: "int", nullable: true, comment: "Estatus de sincronización del registro"),
                    RecordLog_SyncDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ultima fecha de sincronización"),
                    RecordLog_ObjectKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "Código identificador del objeto representado en el registro"),
                    RecordLog_RecordKey = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "Identificador único del registro, asignado en el momento de creación")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionHeaders", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "VisitTypes",
                columns: table => new
                {
                    VisitTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordLog_CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "Usuario que creo el registro"),
                    RecordLog_CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha y hora de creación del registro"),
                    RecordLog_UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "Ultimo usuario que modificó el registro"),
                    RecordLog_UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ultima fecha y hora de actualización del registro"),
                    RecordLog_IsActive = table.Column<bool>(type: "bit", nullable: true, comment: "Registro activo"),
                    RecordLog_IsSystem = table.Column<bool>(type: "bit", nullable: true, comment: "Es un registro del sistema, los registros del sistema no pueden ser eliminados"),
                    RecordLog_SyncStatus = table.Column<int>(type: "int", nullable: true, comment: "Estatus de sincronización del registro"),
                    RecordLog_SyncDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ultima fecha de sincronización"),
                    RecordLog_ObjectKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "Código identificador del objeto representado en el registro"),
                    RecordLog_RecordKey = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "Identificador único del registro, asignado en el momento de creación")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitTypes", x => x.VisitTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RegionDetails",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    NameRoute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecordLog_CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "Usuario que creo el registro"),
                    RecordLog_CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Fecha y hora de creación del registro"),
                    RecordLog_UpdatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true, comment: "Ultimo usuario que modificó el registro"),
                    RecordLog_UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ultima fecha y hora de actualización del registro"),
                    RecordLog_IsActive = table.Column<bool>(type: "bit", nullable: true, comment: "Registro activo"),
                    RecordLog_IsSystem = table.Column<bool>(type: "bit", nullable: true, comment: "Es un registro del sistema, los registros del sistema no pueden ser eliminados"),
                    RecordLog_SyncStatus = table.Column<int>(type: "int", nullable: true, comment: "Estatus de sincronización del registro"),
                    RecordLog_SyncDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Ultima fecha de sincronización"),
                    RecordLog_ObjectKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "Código identificador del objeto representado en el registro"),
                    RecordLog_RecordKey = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true, comment: "Identificador único del registro, asignado en el momento de creación")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionDetails", x => x.RouteId);
                    table.ForeignKey(
                        name: "FK_RegionDetails_RegionHeaders_RegionId",
                        column: x => x.RegionId,
                        principalTable: "RegionHeaders",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegionDetails_RegionId",
                table: "RegionDetails",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegionDetails");

            migrationBuilder.DropTable(
                name: "VisitTypes");

            migrationBuilder.DropTable(
                name: "RegionHeaders");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "VisitHeaders");

            migrationBuilder.DropColumn(
                name: "Routes",
                table: "VisitHeaders");

            migrationBuilder.DropColumn(
                name: "TypeVisit",
                table: "VisitDetails");

            migrationBuilder.AlterColumn<int>(
                name: "TotalVisits",
                table: "VisitHeaders",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
