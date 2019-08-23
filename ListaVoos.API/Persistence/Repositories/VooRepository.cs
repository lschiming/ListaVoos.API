using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaVoos.API.Domain.Dto;
using ListaVoos.API.Domain.Models;
using ListaVoos.API.Domain.Repositories;
using ListaVoos.API.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ListaVoos.API.Persistence.Repositories
{
  public class VooRepository : BaseRepository, IVooRepository
  {
    public VooRepository(DataContext context) : base(context)
    {
    }
    public async Task<List<VooResponseDto>> ListarVoosAsync(DataVooDto voo)
    {


      // Lista todos os voos conforme a Data e a Origem
      var voosSaida = await _context.Voos
        .Where(v => v.Data == voo.Data && v.Origem == voo.Origem)
        .OrderBy(v => v.HoraSaida)
        .ToListAsync();

      // Lista todos os voos conforme a Data e o Destino
      var voosChegada = await _context.Voos
        .Where(v => (v.Data == voo.Data
          || v.Data == DateTime.Parse(voo.Data.ToString()).AddDays(1))
          && v.Destino == voo.Destino)
        .OrderBy(v => v.HoraSaida)
        .ToListAsync();

      // if (!voosSaida.Any() && !voosChegada.Any()) { return NotFound("Nenhum voo encontrado!"); }

      // Lógica para montagem da resposta
      var res = new List<VooResponseDto>();
      for (var i = 0; i < voosSaida.Count; i++)
      {
        var origem = voo.Origem;
        var destino = voo.Destino;
        var horaSaida = voosSaida[i].HoraSaida;

        // Caso o destino seja o desejado, adiciona à lista
        if (voosSaida[i].Destino == voo.Destino)
        {
          var horaChegada = voosSaida[i].HoraChegada;

          var trecho = new VooTrechosDto
          {
            Origem = origem,
            Destino = destino,
            HoraSaida = horaSaida,
            HoraChegada = horaChegada,
            Operadora = voosSaida[i].Operadora,
            Preco = voosSaida[i].Preco
          };

          var listTrechos = new List<VooTrechosDto>();
          listTrechos.Add(trecho);

          var resultado = new VooResponseDto
          {
            Origem = origem,
            Destino = destino,
            HoraSaida = horaSaida,
            HoraChegada = horaChegada,
            Trechos = listTrechos
          };

          // adicionando o resultado à lista
          res.Add(resultado);
        }
        // Caso não seja, verifica as possíveis conexões
        // Máximo de conexões: 1.
        else if (voosChegada.Any())
        {
          for (var j = 0; j < voosChegada.Count; j++)
          {
            if (voosSaida[i].HoraChegada < voosChegada[j].HoraSaida
              && voosSaida[i].HoraChegada.AddHours(12) > voosChegada[j].HoraSaida
              && voosSaida[i].Destino == voosChegada[j].Origem)
            {
              var horaChegada = voosChegada[j].HoraChegada;

              // Monta o primeiro trecho
              var trecho1 = new VooTrechosDto
              {
                Origem = voosSaida[i].Origem,
                Destino = voosSaida[i].Destino,
                HoraSaida = voosSaida[i].HoraSaida,
                HoraChegada = voosSaida[i].HoraChegada,
                Operadora = voosSaida[i].Operadora,
                Preco = voosSaida[i].Preco
              };
              // Criando o segundo trecho
              var trecho2 = new VooTrechosDto
              {
                Origem = voosChegada[j].Origem,
                Destino = voosChegada[j].Destino,
                HoraSaida = voosChegada[j].HoraSaida,
                HoraChegada = voosChegada[j].HoraChegada,
                Operadora = voosChegada[j].Operadora,
                Preco = voosChegada[j].Preco
              };
              // Criando a lista de trechos e adicionando-os
              var listTrechos = new List<VooTrechosDto>();
              listTrechos.Add(trecho1);
              listTrechos.Add(trecho2);

              // Montando a resposta final, no formato solicitado
              var resultado = new VooResponseDto
              {
                Origem = origem,
                Destino = destino,
                HoraSaida = horaSaida,
                HoraChegada = horaChegada,
                Trechos = listTrechos
              };

              res.Add(resultado);
            }
          }
        }

      }

      return res;
    }

    public async Task<Boolean> ValidaPeriodo(DateTime data)
    {
      // Valida se a data está dentro do período em que existem dados
      var inicio = await _context.Voos.OrderBy(i => i.Data).FirstOrDefaultAsync();
      var fim = await _context.Voos.OrderBy(i => i.Data).LastOrDefaultAsync();
      if (data < inicio.Data || data > fim.Data)
      {
        return false;
      }
      return true;
    }
  }
}