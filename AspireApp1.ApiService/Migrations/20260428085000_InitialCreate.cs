using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AspireApp1.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_loueur_lou",
                columns: table => new
                {
                    lou_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lou_nom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    lou_prenom = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    lou_mobile = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    lou_rue = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    lou_cp = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    lou_ville = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    lou_pays = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_loueur_lou", x => x.lou_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_voiture_voi",
                columns: table => new
                {
                    voi_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    voi_immatriculation = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    voi_marque = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    voi_modele = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    voi_categorie = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    voi_prixlocationparjour = table.Column<decimal>(type: "numeric(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_voiture_voi", x => x.voi_id);
                });

            migrationBuilder.CreateTable(
                name: "t_j_location_loc",
                columns: table => new
                {
                    loc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    voi_id = table.Column<int>(type: "integer", nullable: false),
                    lou_id = table.Column<int>(type: "integer", nullable: false),
                    loc_datedebut = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    loc_datefin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_location_loc", x => x.loc_id);
                    table.ForeignKey(
                        name: "FK_t_j_location_loc_t_e_loueur_lou_lou_id",
                        column: x => x.lou_id,
                        principalTable: "t_e_loueur_lou",
                        principalColumn: "lou_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_location_loc_t_e_voiture_voi_voi_id",
                        column: x => x.voi_id,
                        principalTable: "t_e_voiture_voi",
                        principalColumn: "voi_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_loueur_lou_lou_mobile",
                table: "t_e_loueur_lou",
                column: "lou_mobile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_j_location_loc_lou_id",
                table: "t_j_location_loc",
                column: "lou_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_location_loc_voi_id",
                table: "t_j_location_loc",
                column: "voi_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_j_location_loc");

            migrationBuilder.DropTable(
                name: "t_e_loueur_lou");

            migrationBuilder.DropTable(
                name: "t_e_voiture_voi");
        }
    }
}
