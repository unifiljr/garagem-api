using GaragemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GaragemAPI.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<Status> Status { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Descricao = "Disponivel" },
                new Status { Id = 2, Descricao = "Vendido" },
                new Status { Id = 3, Descricao = "Pendente" },
                new Status { Id = 4, Descricao = "Manuntenção" }
            );
        }
    }
}
