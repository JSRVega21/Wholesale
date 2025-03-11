using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wholesale.server.Migrations
{
    /// <inheritdoc />
    public partial class Initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitHeaders",
                columns: table => new
                {
                    VisitHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalVisits = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_VisitHeaders", x => x.VisitHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "VisitDetails",
                columns: table => new
                {
                    VisitDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitHeaderId = table.Column<int>(type: "int", nullable: false),
                    SalespersonCode = table.Column<int>(type: "int", nullable: false),
                    SalespersonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_VisitDetails", x => x.VisitDetailId);
                    table.ForeignKey(
                        name: "FK_VisitDetails_VisitHeaders_VisitHeaderId",
                        column: x => x.VisitHeaderId,
                        principalTable: "VisitHeaders",
                        principalColumn: "VisitHeaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitDetails_VisitHeaderId",
                table: "VisitDetails",
                column: "VisitHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitDetails");

            migrationBuilder.DropTable(
                name: "VisitHeaders");
        }
    }
}
