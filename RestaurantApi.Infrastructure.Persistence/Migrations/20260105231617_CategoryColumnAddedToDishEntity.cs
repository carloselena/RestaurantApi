using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantApi.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CategoryColumnAddedToDishEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Dishes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Dishes");
        }
    }
}
