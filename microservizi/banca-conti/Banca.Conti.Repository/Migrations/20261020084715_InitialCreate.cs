using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banca.Conti.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iban = table.Column<string>(type: "nvarchar(27)", maxLength: 27, nullable: false),
                    Intestatario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaldoContabile = table.Column<float>(type: "real", nullable: false),
                    SaldoDisponibile = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conti", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conti");
        }
    }
}
