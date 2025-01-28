using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOEF.Migrations
{
    /// <inheritdoc />
    public partial class AddProducerFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProducerId",
                table: "Cars",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ProducerId",
                table: "Cars",
                column: "ProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Producers_ProducerId",
                table: "Cars",
                column: "ProducerId",
                principalTable: "Producers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Producers_ProducerId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ProducerId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ProducerId",
                table: "Cars");
        }
    }
}
