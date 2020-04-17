using Microsoft.EntityFrameworkCore.Migrations;

namespace People.Migrations
{
    public partial class idd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Persons_PersonId",
                table: "Children");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Children",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Persons_PersonId",
                table: "Children",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Persons_PersonId",
                table: "Children");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Children",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Persons_PersonId",
                table: "Children",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
