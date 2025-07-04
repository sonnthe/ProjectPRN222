using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectPRN222.Migrations
{
    /// <inheritdoc />
    public partial class SecondSyncWithDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
    name: "FKPackage916382",
    table: "Package");

            migrationBuilder.AddForeignKey(
                name: "FKPackage916382",
                table: "Package",
                column: "subject_id",
                principalTable: "Subject",
                principalColumn: "subject_id",
                onDelete: ReferentialAction.Cascade);


            migrationBuilder.DropForeignKey(
                name: "FKLesson125338",
                table: "Lesson");


            migrationBuilder.AddForeignKey(
                name: "FKLesson125338",
                table: "Lesson",
                column: "subject_id",
                principalTable: "Subject",
                principalColumn: "subject_id",
                onDelete: ReferentialAction.Cascade); 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
