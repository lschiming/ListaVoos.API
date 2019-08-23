using System.Collections.Generic;
using System.Threading.Tasks;
using ListaVoos.API.Domain.Models;
using ListaVoos.API.Domain.Repositories;
using ListaVoos.API.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ListaVoos.API.Persistence.Repositories
{
  public class AeroportoRepository : BaseRepository, IAeroportoRepository
  {
    public AeroportoRepository(DataContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Aeroporto>> ListarAsync()
    {
      return await _context.Aeroportos.ToListAsync();
    }
  }
}