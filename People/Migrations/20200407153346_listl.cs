using Microsoft.EntityFrameworkCore.Migrations;

namespace People.Migrations
{
    public partial class listl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Siblings_PersonId",
                table: "Siblings",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Siblings_Persons_PersonId",
                table: "Siblings",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Siblings_Persons_PersonId",
                table: "Siblings");

            migrationBuilder.DropIndex(
                name: "IX_Siblings_PersonId",
                table: "Siblings");
        }
    }
}
