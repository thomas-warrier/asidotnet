using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireApp1.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLoueurModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "lou_blackliste",
                table: "t_e_loueur_lou",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lou_blackliste",
                table: "t_e_loueur_lou");
        }
    }
}
