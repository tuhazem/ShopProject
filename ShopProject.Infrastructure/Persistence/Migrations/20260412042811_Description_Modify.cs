using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProject.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Description_Modify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Decripition",
                table: "Products",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "Decripition");
        }
    }
}
