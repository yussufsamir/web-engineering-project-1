using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitness_tracker.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WorkoutAssignments_AthleteId",
                table: "WorkoutAssignments",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutAssignments_WodId",
                table: "WorkoutAssignments",
                column: "WodId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutAssignments_Athletes_AthleteId",
                table: "WorkoutAssignments",
                column: "AthleteId",
                principalTable: "Athletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutAssignments_Wods_WodId",
                table: "WorkoutAssignments",
                column: "WodId",
                principalTable: "Wods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutAssignments_Athletes_AthleteId",
                table: "WorkoutAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutAssignments_Wods_WodId",
                table: "WorkoutAssignments");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutAssignments_AthleteId",
                table: "WorkoutAssignments");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutAssignments_WodId",
                table: "WorkoutAssignments");
        }
    }
}
