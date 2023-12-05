using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pizzaria_reges.Migrations
{
    /// <inheritdoc />
    public partial class update_produto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "descricao",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "urlImg",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "descricao",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "urlImg",
                table: "Produto");
        }
    }
}
