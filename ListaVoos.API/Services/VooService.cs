using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ListaVoos.API.Domain.Dto;
using ListaVoos.API.Domain.Repositories;
using ListaVoos.API.Domain.Services;

namespace ListaVoos.API.Services
{
  public class VooService : IVooService
  {
    private readonly IVooRepository _vooRepository;
    public VooService(IVooRepository vooRepository)
    {
      _vooRepository = vooRepository;
    }

    public async Task<List<VooResponseDto>> ListarVoosAsync(DataVooDto voo)
    {
      return await _vooRepository.ListarVoosAsync(voo);
    }

    public async Task<Boolean> ValidaPeriodo(DateTime data)
    {
      return await _vooRepository.ValidaPeriodo(data);
    }
  }
}