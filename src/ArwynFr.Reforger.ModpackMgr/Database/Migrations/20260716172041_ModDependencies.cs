using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArwynFr.Reforger.ModpackMgr.Database.Migrations
{
    /// <inheritdoc />
    public partial class ModDependencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModMod",
                columns: table => new
                {
                    DependantsId = table.Column<string>(type: "TEXT", nullable: false),
                    DependenciesId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModMod", x => new { x.DependantsId, x.DependenciesId });
                    table.ForeignKey(
                        name: "FK_ModMod_Mods_DependantsId",
                        column: x => x.DependantsId,
                        principalTable: "Mods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModMod_Mods_DependenciesId",
                        column: x => x.DependenciesId,
                        principalTable: "Mods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModMod_DependenciesId",
                table: "ModMod",
                column: "DependenciesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModMod");
        }
    }
}
