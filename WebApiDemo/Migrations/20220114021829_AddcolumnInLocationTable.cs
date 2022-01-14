using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiDemo.Migrations
{
    public partial class AddcolumnInLocationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_StudentId",
                table: "Locations",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Students_StudentId",
                table: "Locations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Students_StudentId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_StudentId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Locations");
        }
    }
}
