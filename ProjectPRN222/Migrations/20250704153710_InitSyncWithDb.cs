using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectPRN222.Migrations
{
    /// <inheritdoc />
    public partial class InitSyncWithDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_Lesson_Topic_Subject",
            table: "Lesson_Topic");

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Topic_Subject",
                table: "Lesson_Topic",
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
