using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pizzaria_reges.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endereco_cli",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cep = table.Column<int>(type: "int", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco_cli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<float>(type: "real", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_nasc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cpf = table.Column<int>(type: "int", nullable: false),
                    fk_EnderecoID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_Endereco_cli_fk_EnderecoID",
                        column: x => x.fk_EnderecoID,
                        principalTable: "Endereco_cli",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ValorTotal = table.Column<float>(type: "real", nullable: true),
                    PedidoData = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fk_ClienteID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedido_Cliente_fk_ClienteID",
                        column: x => x.fk_ClienteID,
                        principalTable: "Cliente",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoCompra",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fk_PedidoID = table.Column<long>(type: "bigint", nullable: true),
                    fk_ProdutoID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrinhoCompra_Pedido_fk_PedidoID",
                        column: x => x.fk_PedidoID,
                        principalTable: "Pedido",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CarrinhoCompra_Produto_fk_ProdutoID",
                        column: x => x.fk_ProdutoID,
                        principalTable: "Produto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompra_fk_PedidoID",
                table: "CarrinhoCompra",
                column: "fk_PedidoID");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompra_fk_ProdutoID",
                table: "CarrinhoCompra",
                column: "fk_ProdutoID");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_fk_EnderecoID",
                table: "Cliente",
                column: "fk_EnderecoID",
                unique: true,
                filter: "[fk_EnderecoID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_fk_ClienteID",
                table: "Pedido",
                column: "fk_ClienteID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoCompra");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Endereco_cli");
        }
    }
}
