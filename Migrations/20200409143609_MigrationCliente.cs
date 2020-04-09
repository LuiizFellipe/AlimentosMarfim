using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlimentosMarfim.Migrations
{
    public partial class MigrationCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeCliente = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(nullable: false),
                    Telefone = table.Column<string>(nullable: false),
                    Endereco = table.Column<string>(nullable: false),
                    NumCasa = table.Column<string>(nullable: false),
                    Complemeto = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: false),
                    Cidade = table.Column<string>(nullable: false),
                    Uf = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
