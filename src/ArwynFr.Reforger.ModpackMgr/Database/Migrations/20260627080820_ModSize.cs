using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArwynFr.Reforger.ModpackMgr.Database.Migrations
{
    /// <inheritdoc />
    public partial class ModSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "WorkshopInformation_Size",
                table: "Mods",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkshopInformation_Size",
                table: "Mods");
        }
    }
}
