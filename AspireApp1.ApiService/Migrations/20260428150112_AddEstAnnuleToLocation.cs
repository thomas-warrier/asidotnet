using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireApp1.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddEstAnnuleToLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "loc_annule",
                table: "t_j_location_loc",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loc_annule",
                table: "t_j_location_loc");
        }
    }
}
