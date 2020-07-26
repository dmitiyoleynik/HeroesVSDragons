using Microsoft.EntityFrameworkCore.Migrations;

namespace DragonLibrary_.Migrations
{
    public partial class Addidtohit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Hits",
                table: "Hits");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Hits",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hits",
                table: "Hits",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Hits_HeroId",
                table: "Hits",
                column: "HeroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Hits",
                table: "Hits");

            migrationBuilder.DropIndex(
                name: "IX_Hits_HeroId",
                table: "Hits");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Hits");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hits",
                table: "Hits",
                columns: new[] { "HeroId", "DragonId" });
        }
    }
}
