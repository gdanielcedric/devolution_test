using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "assurproduct",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Garanties = table.Column<List<string>>(type: "text[]", nullable: false),
                    Categories = table.Column<List<string>>(type: "text[]", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assurproduct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "categoryvehicle",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Libelle = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categoryvehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "subscription",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    IdSubscriber = table.Column<string>(type: "text", nullable: false),
                    IdVehicle = table.Column<string>(type: "text", nullable: false),
                    IdAssurProduct = table.Column<string>(type: "text", nullable: false),
                    QuoteReference = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Step = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "suscriber",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Telephone = table.Column<string>(type: "text", nullable: false),
                    Nom = table.Column<string>(type: "text", nullable: false),
                    Prenom = table.Column<string>(type: "text", nullable: false),
                    CNI = table.Column<string>(type: "text", nullable: false),
                    Ville = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_suscriber", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vehicle",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    DateFirstCirculation = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ImmatriculationNumber = table.Column<string>(type: "text", nullable: false),
                    Couleur = table.Column<string>(type: "text", nullable: false),
                    NombreSiege = table.Column<int>(type: "integer", nullable: false),
                    NombrePorte = table.Column<int>(type: "integer", nullable: false),
                    IdCategoryVehicle = table.Column<string>(type: "text", nullable: false),
                    IdSuscriber = table.Column<string>(type: "text", nullable: false),
                    ValueNeuve = table.Column<double>(type: "double precision", nullable: false),
                    ValueVenale = table.Column<double>(type: "double precision", nullable: false),
                    FiscalPower = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehicle", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "assurproduct_pk",
                schema: "public",
                table: "assurproduct",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "categoryvehicle_pk",
                schema: "public",
                table: "categoryvehicle",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "assurproduct_fk",
                schema: "public",
                table: "subscription",
                column: "IdAssurProduct");

            migrationBuilder.CreateIndex(
                name: "subscription_pk",
                schema: "public",
                table: "subscription",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "suscriber_fk",
                schema: "public",
                table: "subscription",
                column: "IdSubscriber");

            migrationBuilder.CreateIndex(
                name: "vehicle_fk",
                schema: "public",
                table: "subscription",
                column: "IdVehicle");

            migrationBuilder.CreateIndex(
                name: "suscriber_pk",
                schema: "public",
                table: "suscriber",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "categoryvehicle_fk",
                schema: "public",
                table: "vehicle",
                column: "IdCategoryVehicle");

            migrationBuilder.CreateIndex(
                name: "suscriber_fk1",
                schema: "public",
                table: "vehicle",
                column: "IdSuscriber");

            migrationBuilder.CreateIndex(
                name: "vehicle_pk",
                schema: "public",
                table: "vehicle",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assurproduct",
                schema: "public");

            migrationBuilder.DropTable(
                name: "categoryvehicle",
                schema: "public");

            migrationBuilder.DropTable(
                name: "subscription",
                schema: "public");

            migrationBuilder.DropTable(
                name: "suscriber",
                schema: "public");

            migrationBuilder.DropTable(
                name: "vehicle",
                schema: "public");
        }
    }
}
