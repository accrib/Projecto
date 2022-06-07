using Microsoft.EntityFrameworkCore.Migrations;

namespace _01_DAL.Migrations
{
    public partial class migration_16_09_2019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codigo_Fatura",
                table: "Faturas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "codigo_Fatura",
                table: "Faturas",
                nullable: true);
        }
    }
}
