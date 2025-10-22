using Microsoft.EntityFrameworkCore;
using Itens.Models;


namespace Itens.Data;

public class AppDbContext : DbContext
{
    public AppDbContext() { }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Item> Itens => Set<Item>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=rpg.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(e =>
        {
            e.HasKey(i => i.Id);
            e.Property(i => i.Nome).IsRequired().HasMaxLength(100);
            e.Property(i => i.Raridade).IsRequired().HasMaxLength(50);
            e.Property(i => i.Preco).HasColumnType("decimal(10,2)");
        });
    }
}
