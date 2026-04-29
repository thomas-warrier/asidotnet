using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireApp1.ApiService.Migrations
{
    /// <inheritdoc />
    public partial class AddEstPayeToLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "loc_paye",
                table: "t_j_location_loc",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loc_paye",
                table: "t_j_location_loc");
        }
    }
}
