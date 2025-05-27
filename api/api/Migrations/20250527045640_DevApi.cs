using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class DevApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "assurproduct",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
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
                name: "subscription",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    IdSubscriber = table.Column<string>(type: "text", nullable: false),
                    IdVehicle = table.Column<string>(type: "text", nullable: false),
                    IdAssurProduct = table.Column<string>(type: "text", nullable: false),
                    quoteReference = table.Column<string>(type: "text", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "categoryvehicle",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Libelle = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AssurProductId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categoryvehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_categoryvehicle_assurproduct_AssurProductId",
                        column: x => x.AssurProductId,
                        principalTable: "assurproduct",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Guaranty",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AssurProductId = table.Column<string>(type: "text", nullable: true),
                    GuarantyType = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    CollisionTierceGuaranty_Value = table.Column<double>(type: "double precision", nullable: true),
                    Value = table.Column<double>(type: "double precision", nullable: true),
                    IncendieGuaranty_Value = table.Column<double>(type: "double precision", nullable: true),
                    PlafondTierceGuaranty_Value = table.Column<double>(type: "double precision", nullable: true),
                    FiscalPower = table.Column<int>(type: "integer", nullable: true),
                    VolGuaranty_Value = table.Column<double>(type: "double precision", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guaranty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guaranty_assurproduct_AssurProductId",
                        column: x => x.AssurProductId,
                        principalTable: "assurproduct",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "assurproduct_pk",
                table: "assurproduct",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "categoryvehicle_pk",
                table: "categoryvehicle",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_categoryvehicle_AssurProductId",
                table: "categoryvehicle",
                column: "AssurProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Guaranty_AssurProductId",
                table: "Guaranty",
                column: "AssurProductId");

            migrationBuilder.CreateIndex(
                name: "assurproduct_fk",
                table: "subscription",
                column: "IdAssurProduct");

            migrationBuilder.CreateIndex(
                name: "subscription_pk",
                table: "subscription",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "suscriber_fk",
                table: "subscription",
                column: "IdSubscriber");

            migrationBuilder.CreateIndex(
                name: "vehicle_fk",
                table: "subscription",
                column: "IdVehicle");

            migrationBuilder.CreateIndex(
                name: "suscriber_pk",
                table: "suscriber",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "categoryvehicle_fk",
                table: "vehicle",
                column: "IdCategoryVehicle");

            migrationBuilder.CreateIndex(
                name: "suscriber_fk1",
                table: "vehicle",
                column: "IdSuscriber");

            migrationBuilder.CreateIndex(
                name: "vehicle_pk",
                table: "vehicle",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "categoryvehicle");

            migrationBuilder.DropTable(
                name: "Guaranty");

            migrationBuilder.DropTable(
                name: "subscription");

            migrationBuilder.DropTable(
                name: "suscriber");

            migrationBuilder.DropTable(
                name: "vehicle");

            migrationBuilder.DropTable(
                name: "assurproduct");
        }
    }
}
