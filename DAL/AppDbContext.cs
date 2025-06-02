using api.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.DAL;

public class AppDbContext : DbContext
{
  public DbSet<TbUsuario> Usuarios { get; set; }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseNpgsql("");
  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }
}