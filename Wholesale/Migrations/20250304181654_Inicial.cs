using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wholesale.server.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserWholesale",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_UserWholesale", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWholesale");
        }
    }
}
