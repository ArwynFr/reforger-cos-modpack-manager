using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArwynFr.Reforger.ModpackMgr.Database.Migrations
{
    /// <inheritdoc />
    public partial class Mods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Version = table.Column<string>(type: "TEXT", nullable: false),
                    Enabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    WorkshopInformation_Id = table.Column<string>(type: "TEXT", nullable: true),
                    WorkshopInformation_Name = table.Column<string>(type: "TEXT", nullable: true),
                    WorkshopInformation_Version = table.Column<string>(type: "TEXT", nullable: true),
                    WorkshopInformation_GameVersion = table.Column<string>(type: "TEXT", nullable: true),
                    WorkshopInformation_Dependencies = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mods", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mods");
        }
    }
}
