using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ListaVoos.API.Domain.Dto
{
  public class VooResponseDto
  {
    public string Origem { get; set; }
    public string Destino { get; set; }

    [JsonProperty("saida")]
    public DateTime HoraSaida { get; set; }

    [JsonProperty("chegada")]
    public DateTime HoraChegada { get; set; }
    public List<VooTrechosDto> Trechos { get; set; }
  }

}