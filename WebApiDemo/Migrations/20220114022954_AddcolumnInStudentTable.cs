using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiDemo.Migrations
{
    public partial class AddcolumnInStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Locations_StudentId",
                table: "Locations");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_StudentId",
                table: "Locations",
                column: "StudentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Locations_StudentId",
                table: "Locations");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_StudentId",
                table: "Locations",
                column: "StudentId");
        }
    }
}
