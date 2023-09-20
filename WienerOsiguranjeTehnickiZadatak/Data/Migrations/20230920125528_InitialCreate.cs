using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WienerOsiguranjeTehnickiZadatak.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Domicilnost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domicilnost", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipPartnera",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipPartnera", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipPartneraId = table.Column<int>(type: "int", nullable: false),
                    DomicilnostId = table.Column<int>(type: "int", nullable: false),
                    OIB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Spol = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EksterniBrojPartnera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumUnosa = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partner_Domicilnost_DomicilnostId",
                        column: x => x.DomicilnostId,
                        principalTable: "Domicilnost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partner_TipPartnera_TipPartneraId",
                        column: x => x.TipPartneraId,
                        principalTable: "TipPartnera",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Polica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojPolice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IznosPremije = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polica_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partner_DomicilnostId",
                table: "Partner",
                column: "DomicilnostId");

            migrationBuilder.CreateIndex(
                name: "IX_Partner_TipPartneraId",
                table: "Partner",
                column: "TipPartneraId");

            migrationBuilder.CreateIndex(
                name: "IX_Polica_PartnerId",
                table: "Polica",
                column: "PartnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Polica");

            migrationBuilder.DropTable(
                name: "Partner");

            migrationBuilder.DropTable(
                name: "Domicilnost");

            migrationBuilder.DropTable(
                name: "TipPartnera");
        }
    }
}
