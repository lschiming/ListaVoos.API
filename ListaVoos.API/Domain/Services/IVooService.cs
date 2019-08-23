using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ListaVoos.API.Domain.Dto;

namespace ListaVoos.API.Domain.Services
{
  public interface IVooService
  {
    Task<List<VooResponseDto>> ListarVoosAsync(DataVooDto voo);
    Task<Boolean> ValidaPeriodo(DateTime data);
  }
}