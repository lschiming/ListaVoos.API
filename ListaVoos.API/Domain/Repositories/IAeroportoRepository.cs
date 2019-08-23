using System.Collections.Generic;
using System.Threading.Tasks;
using ListaVoos.API.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListaVoos.API.Domain.Repositories
{
  public interface IAeroportoRepository
  {
    Task<IEnumerable<Aeroporto>> ListarAsync();
  }
}