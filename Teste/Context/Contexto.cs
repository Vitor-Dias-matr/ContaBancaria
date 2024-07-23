using Microsoft.EntityFrameworkCore;
using Teste.Models;

namespace Teste.Context
{
    public class Contexto : DbContext
    {
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Movimentacao> Movimentacoes { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conta>(entity =>
            {
                entity.HasKey(c => c.NumeroConta);
                entity.Property(c => c.Responsavel).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Saldo).IsRequired().HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Movimentacao>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Tipo).IsRequired();
                entity.Property(m => m.Data).IsRequired();
                entity.Property(m => m.Valor).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(m => m.NumeroContaOrigem).IsRequired();
                entity.Property(m => m.NumeroContaDestino);
            });
        }
    }
}
