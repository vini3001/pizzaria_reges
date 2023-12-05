using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pizzaria_reges.Models;

namespace pizzaria_reges.Data
{
    public class IESContext : DbContext
    {
            public IESContext(DbContextOptions<IESContext> options) : base(options)
            {
            Database.EnsureCreated();
            }

            public DbSet<Cliente> Cliente { get; set; }
            public DbSet<Endereco_cli> Endereco_cli { get; set; }
            public DbSet<Pedido> Pedido { get; set; }
            public DbSet<Produto> Produto { get; set; }
            public DbSet<CarrinhoCompra> CarrinhoCompra { get; set; }
    }
}
