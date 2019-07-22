using Microsoft.EntityFrameworkCore;
using PDV.Domain.Entities;

namespace PDV.Infra.Data.Context
{
    public class PdvContext : DbContext
    {
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var db = optionsBuilder.UseMySql("server=localhost;port=6603;database=dbmysql;user=root;password=db123456");
            }
        }
    }
}
