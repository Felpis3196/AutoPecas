using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimasTurbo.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RelatMov",
                columns: table => new
                {
                    RelatMovId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotaFiscal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoMovimentacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Produto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatMov", x => x.RelatMovId);
                });

            migrationBuilder.CreateTable(
                name: "tbCategoria",
                columns: table => new
                {
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCategoria", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "tbCliente",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCliente", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "tbFornecedor",
                columns: table => new
                {
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbFornecedor", x => x.FornecedorId);
                });

            migrationBuilder.CreateTable(
                name: "tbProduto",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PreçoUnit = table.Column<float>(type: "real", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProduto", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_tbProduto_tbCategoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "tbCategoria",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbProduto_tbFornecedor_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "tbFornecedor",
                        principalColumn: "FornecedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbCadCompras",
                columns: table => new
                {
                    CadComprasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotaCompra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCadCompras", x => x.CadComprasId);
                    table.ForeignKey(
                        name: "FK_tbCadCompras_tbProduto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "tbProduto",
                        principalColumn: "ProdutoId");
                });

            migrationBuilder.CreateTable(
                name: "tbCadVendas",
                columns: table => new
                {
                    CadVendasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotaVenda = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCadVendas", x => x.CadVendasId);
                    table.ForeignKey(
                        name: "FK_tbCadVendas_tbCliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "tbCliente",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbCadVendas_tbProduto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "tbProduto",
                        principalColumn: "ProdutoId");
                });

            migrationBuilder.CreateTable(
                name: "tbItemCompra",
                columns: table => new
                {
                    ItemCompraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrecoUnit = table.Column<float>(type: "real", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    CadComprasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbItemCompra", x => x.ItemCompraId);
                    table.ForeignKey(
                        name: "FK_tbItemCompra_tbCadCompras_CadComprasId",
                        column: x => x.CadComprasId,
                        principalTable: "tbCadCompras",
                        principalColumn: "CadComprasId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbItemCompra_tbProduto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "tbProduto",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbItemVenda",
                columns: table => new
                {
                    ItemVendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrecoUnit = table.Column<float>(type: "real", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    CadVendasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbItemVenda", x => x.ItemVendaId);
                    table.ForeignKey(
                        name: "FK_tbItemVenda_tbCadVendas_CadVendasId",
                        column: x => x.CadVendasId,
                        principalTable: "tbCadVendas",
                        principalColumn: "CadVendasId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbItemVenda_tbProduto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "tbProduto",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbCadCompras_ProdutoId",
                table: "tbCadCompras",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbCadVendas_ClienteId",
                table: "tbCadVendas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_tbCadVendas_ProdutoId",
                table: "tbCadVendas",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbItemCompra_CadComprasId",
                table: "tbItemCompra",
                column: "CadComprasId");

            migrationBuilder.CreateIndex(
                name: "IX_tbItemCompra_ProdutoId",
                table: "tbItemCompra",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbItemVenda_CadVendasId",
                table: "tbItemVenda",
                column: "CadVendasId");

            migrationBuilder.CreateIndex(
                name: "IX_tbItemVenda_ProdutoId",
                table: "tbItemVenda",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbProduto_CategoriaId",
                table: "tbProduto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbProduto_FornecedorId",
                table: "tbProduto",
                column: "FornecedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatMov");

            migrationBuilder.DropTable(
                name: "tbItemCompra");

            migrationBuilder.DropTable(
                name: "tbItemVenda");

            migrationBuilder.DropTable(
                name: "tbCadCompras");

            migrationBuilder.DropTable(
                name: "tbCadVendas");

            migrationBuilder.DropTable(
                name: "tbCliente");

            migrationBuilder.DropTable(
                name: "tbProduto");

            migrationBuilder.DropTable(
                name: "tbCategoria");

            migrationBuilder.DropTable(
                name: "tbFornecedor");
        }
    }
}
