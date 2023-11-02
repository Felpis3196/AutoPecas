using Microsoft.EntityFrameworkCore;
using SimasTurbo.Models;

namespace SimasTurbo.Data
{
    public class SimasContext : DbContext
    {
        public SimasContext(DbContextOptions<SimasContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CadCompras> CadCompras { get; set; }
        public DbSet<CadVendas> CadVendas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<ItemVenda> ItemVendas { get; set; }
        public DbSet<ItemCompra> ItemCompras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("tbCliente");
            modelBuilder.Entity<CadCompras>().ToTable("tbCadCompras");
            modelBuilder.Entity<CadVendas>().ToTable("tbCadVendas");
            modelBuilder.Entity<Categoria>().ToTable("tbCategoria");
            modelBuilder.Entity<Fornecedor>().ToTable("tbFornecedor");
            modelBuilder.Entity<Produto>().ToTable("tbProduto");
            modelBuilder.Entity<ItemVenda>().ToTable("tbItemVenda");
            modelBuilder.Entity<ItemCompra>().ToTable("tbItemCompra");


            
        }


        public DbSet<SimasTurbo.Models.ItemCompra> ItemCompra { get; set; } = default!;

        public DbSet<SimasTurbo.Models.ItemVenda> ItemVenda { get; set; } = default!;

        public DbSet<SimasTurbo.Models.RelatMov> RelatMov { get; set; } = default!;

        public DbSet<SimasTurbo.Models.RelatProd> RelatProd { get; set; } = default!;
    }
}
