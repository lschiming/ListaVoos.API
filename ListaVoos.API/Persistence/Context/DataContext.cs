using System;
using System.Threading.Tasks;
using ListaVoos.API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaVoos.API.Persistence.Context
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Aeroporto> Aeroportos { get; set; }
    public DbSet<Voo> Voos { get; set; }

  }
}