using System.Collections.Generic;
using System.Threading.Tasks;
using ListaVoos.API.Domain.Models;
using ListaVoos.API.Domain.Repositories;
using ListaVoos.API.Domain.Services;

namespace ListaVoos.API.Services
{
  public class AeroportoService : IAeroportoService
  {
    private readonly IAeroportoRepository _aeroportoRepository;
    public AeroportoService(IAeroportoRepository aeroportoRepository)
    {
      _aeroportoRepository = aeroportoRepository;
    }

    public async Task<IEnumerable<Aeroporto>> ListarAsync()
    {
      return await _aeroportoRepository.ListarAsync();
    }
  }
}