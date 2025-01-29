using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAOSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddProducersData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Producers (Name) VALUES ('Producer 1')");
            migrationBuilder.Sql("INSERT INTO Producers (Name) VALUES ('Producer 2')");
            migrationBuilder.Sql("INSERT INTO Shoes (Name, Description, ProducerId, ReleaseYear, ShoeType) VALUES ('Shoe 1', 'comfy', 1, 2005, 1)");
            migrationBuilder.Sql("INSERT INTO Shoes (Name, Description, ProducerId, ReleaseYear, ShoeType) VALUES ('Shoe 2', 'comfy', 1, 2007, 0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
