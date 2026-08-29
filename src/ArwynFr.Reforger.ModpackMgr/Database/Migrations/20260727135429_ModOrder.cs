using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArwynFr.Reforger.ModpackMgr.Database.Migrations
{
    /// <inheritdoc />
    public partial class ModOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Mods",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Mods");
        }
    }
}
