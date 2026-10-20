using Banca.Conti.Repository.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca.Conti.Repository
{
    public class ContiDbContext:  DbContext
    {
        public ContiDbContext() { }
        public ContiDbContext(DbContextOptions<ContiDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Se le opzioni non sono già state configurate dalla Dependency Injection in Program.cs
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ContiDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conto>(entity =>
            {
                // Definizione chiave primaria
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                // Configurazione IBAN (lunghezza standard massima 27 caratteri)
                entity.Property(e => e.Iban)
                      .IsRequired()
                      .HasMaxLength(27);

            });
        }
        public DbSet<Conto> Conti { get; set; }
    }
}
