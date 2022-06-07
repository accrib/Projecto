using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _01_DAL.Migrations
{
    public partial class migration_12_09_2019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empregados",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Numero_Empregado = table.Column<int>(nullable: false),
                    EMail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empregados", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Descricao_Produto = table.Column<string>(nullable: true),
                    ID_Empregado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Produtos_Empregados_ID_Empregado",
                        column: x => x.ID_Empregado,
                        principalTable: "Empregados",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faturas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Data_Fatura = table.Column<DateTime>(nullable: false),
                    ID_Empregado = table.Column<int>(nullable: false),
                    Preco_Final = table.Column<double>(nullable: false),
                    ProdutoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Faturas_Empregados_ID_Empregado",
                        column: x => x.ID_Empregado,
                        principalTable: "Empregados",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Faturas_Produtos_ProdutoID",
                        column: x => x.ProdutoID,
                        principalTable: "Produtos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Linhas_Fatura",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Quantidade_Produto = table.Column<int>(nullable: false),
                    Preco_Produto = table.Column<double>(nullable: false),
                    ID_Produto = table.Column<int>(nullable: false),
                    FaturaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linhas_Fatura", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Linhas_Fatura_Faturas_FaturaID",
                        column: x => x.FaturaID,
                        principalTable: "Faturas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Linhas_Fatura_Produtos_ID_Produto",
                        column: x => x.ID_Produto,
                        principalTable: "Produtos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_ID_Empregado",
                table: "Faturas",
                column: "ID_Empregado");

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_ProdutoID",
                table: "Faturas",
                column: "ProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Linhas_Fatura_FaturaID",
                table: "Linhas_Fatura",
                column: "FaturaID");

            migrationBuilder.CreateIndex(
                name: "IX_Linhas_Fatura_ID_Produto",
                table: "Linhas_Fatura",
                column: "ID_Produto");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_ID_Empregado",
                table: "Produtos",
                column: "ID_Empregado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Linhas_Fatura");

            migrationBuilder.DropTable(
                name: "Faturas");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Empregados");
        }
    }
}
