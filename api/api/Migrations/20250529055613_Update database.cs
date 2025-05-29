using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "vehicle_fk",
                schema: "public",
                table: "subscription",
                newName: "vehicle_fk1");

            migrationBuilder.RenameIndex(
                name: "assurproduct_fk",
                schema: "public",
                table: "subscription",
                newName: "assurproduct_fk1");

            migrationBuilder.CreateTable(
                name: "simulation",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    IdVehicle = table.Column<string>(type: "text", nullable: false),
                    IdAssurProduct = table.Column<string>(type: "text", nullable: false),
                    QuoteReference = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_simulation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "assurproduct_fk",
                schema: "public",
                table: "simulation",
                column: "IdAssurProduct");

            migrationBuilder.CreateIndex(
                name: "simulation_pk",
                schema: "public",
                table: "simulation",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "vehicle_fk",
                schema: "public",
                table: "simulation",
                column: "IdVehicle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "simulation",
                schema: "public");

            migrationBuilder.RenameIndex(
                name: "vehicle_fk1",
                schema: "public",
                table: "subscription",
                newName: "vehicle_fk");

            migrationBuilder.RenameIndex(
                name: "assurproduct_fk1",
                schema: "public",
                table: "subscription",
                newName: "assurproduct_fk");
        }
    }
}
