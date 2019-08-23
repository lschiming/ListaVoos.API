using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaVoos.API.Persistence.Context;
using ListaVoos.API.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ListaVoos.API.Domain.Dto;
using ListaVoos.API.Services;
using ListaVoos.API.Domain.Services;

namespace ListaVoos.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class VoosController : ControllerBase
  {
    private readonly IVooService _vooService;
    public VoosController(IVooService vooService)
    {
      _vooService = vooService;
    }

    // private readonly DataContext _context;
    // public VoosController(DataContext context)
    // {
    //   _context = context;
    // }

    /// <summary>
    /// Retorna uma lista de voos conforme especificado.
    /// </summary>
    /// <remarks>
    /// Amostra de requisição:
    ///
    ///     POST /voos
    ///     {
    ///         "origem": "MCZ",
    ///         "destino": "FLN",
    ///         "data": "2018-02-10"
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] DataVooDto voo)
    {
      // Valida se as informações foram fornecidas de acordo com o modelo (DataVooDto)
      if (!ModelState.IsValid) { return BadRequest(ModelState); }

      // Valida se a data está no período
      var data = DateTime.Parse(voo.Data.ToString());
      if (!(await _vooService.ValidaPeriodo(data)))
      {
        return BadRequest(String.Format("Data fora do período especificado. " +
          "Valor fornecido: {0:d}", data));
      };

      // Executa a verificação
      var voos = await _vooService.ListarVoosAsync(voo);

      // Valida se foram encontrados voos que atendem à solicitação
      if (voos.Count == 0) { return NotFound("Nenhum voo encontrado!"); }

      return Ok(voos);
    }

  }
}