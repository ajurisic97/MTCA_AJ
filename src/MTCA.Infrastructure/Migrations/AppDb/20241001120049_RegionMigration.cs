using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTCA.Infrastructure.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class RegionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityRegions",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "CountryRegions",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "StreetRegions",
                schema: "Data");

            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                schema: "Catalog",
                table: "Streets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                schema: "Catalog",
                table: "Countries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                schema: "Catalog",
                table: "Cities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Streets_RegionId",
                schema: "Catalog",
                table: "Streets",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_RegionId",
                schema: "Catalog",
                table: "Countries",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionId",
                schema: "Catalog",
                table: "Cities",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Regions_RegionId",
                schema: "Catalog",
                table: "Cities",
                column: "RegionId",
                principalSchema: "Catalog",
                principalTable: "Regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Regions_RegionId",
                schema: "Catalog",
                table: "Countries",
                column: "RegionId",
                principalSchema: "Catalog",
                principalTable: "Regions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Streets_Regions_RegionId",
                schema: "Catalog",
                table: "Streets",
                column: "RegionId",
                principalSchema: "Catalog",
                principalTable: "Regions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Regions_RegionId",
                schema: "Catalog",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Regions_RegionId",
                schema: "Catalog",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_Streets_Regions_RegionId",
                schema: "Catalog",
                table: "Streets");

            migrationBuilder.DropIndex(
                name: "IX_Streets_RegionId",
                schema: "Catalog",
                table: "Streets");

            migrationBuilder.DropIndex(
                name: "IX_Countries_RegionId",
                schema: "Catalog",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Cities_RegionId",
                schema: "Catalog",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "RegionId",
                schema: "Catalog",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "RegionId",
                schema: "Catalog",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "RegionId",
                schema: "Catalog",
                table: "Cities");

            migrationBuilder.EnsureSchema(
                name: "Data");

            migrationBuilder.CreateTable(
                name: "CityRegions",
                schema: "Data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityRegions_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "Catalog",
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CityRegions_Regions_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "Catalog",
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountryRegions",
                schema: "Data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryRegions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Catalog",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountryRegions_Regions_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "Catalog",
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StreetRegions",
                schema: "Data",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StreetRegions_Regions_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "Catalog",
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StreetRegions_Streets_StreetId",
                        column: x => x.StreetId,
                        principalSchema: "Catalog",
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityRegions_CityId",
                schema: "Data",
                table: "CityRegions",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CityRegions_RegionId",
                schema: "Data",
                table: "CityRegions",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryRegions_CountryId",
                schema: "Data",
                table: "CountryRegions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryRegions_RegionId",
                schema: "Data",
                table: "CountryRegions",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_StreetRegions_RegionId",
                schema: "Data",
                table: "StreetRegions",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_StreetRegions_StreetId",
                schema: "Data",
                table: "StreetRegions",
                column: "StreetId");
        }
    }
}
