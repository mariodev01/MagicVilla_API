using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class LlenandoLaTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "villas",
                columns: new[] { "Id", "Amenidad", "Detalles", "ImagenUrl", "MetrosCuadrados", "Name", "Ocupantes", "Tarifa", "fechaActualizacion", "fechaCreacion" },
                values: new object[] { 1, "", "la verdadera villa", "", 400, "villa la parra", 4, 1400.0, new DateTime(2023, 2, 10, 23, 49, 1, 963, DateTimeKind.Local).AddTicks(3412), new DateTime(2023, 2, 10, 23, 49, 1, 963, DateTimeKind.Local).AddTicks(3400) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
