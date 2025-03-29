using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LectureAPI.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseRelationshipUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_Courses_CourseId",
                table: "StudentCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_Students_StudentId",
                table: "StudentCourse");

            migrationBuilder.AddColumn<int>(
                name: "CourseId1",
                table: "StudentCourse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_CourseId1",
                table: "StudentCourse",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_Courses_CourseId1",
                table: "StudentCourse",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_Students_CourseId",
                table: "StudentCourse",
                column: "CourseId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_Courses_CourseId1",
                table: "StudentCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_Students_CourseId",
                table: "StudentCourse");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourse_CourseId1",
                table: "StudentCourse");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "StudentCourse");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_Courses_CourseId",
                table: "StudentCourse",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_Students_StudentId",
                table: "StudentCourse",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
