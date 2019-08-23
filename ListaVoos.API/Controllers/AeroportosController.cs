using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaVoos.API.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ListaVoos.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AeroportosController : ControllerBase
  {
    private readonly IAeroportoService _aeroportoService;
    public AeroportosController(IAeroportoService aeroportoService)
    {
      _aeroportoService = aeroportoService;
    }

    /// <summary>
    /// Retorna uma lista de aeroportos cadastrados.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
      var aeroportos = await _aeroportoService.ListarAsync();
      return Ok(aeroportos);
    }

  }
}