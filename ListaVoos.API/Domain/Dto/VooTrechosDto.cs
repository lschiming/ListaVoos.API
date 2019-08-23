using System;
using Newtonsoft.Json;

namespace ListaVoos.API.Domain.Dto
{
  public class VooTrechosDto
  {
    public string Origem { get; set; }
    public string Destino { get; set; }

    [JsonProperty("saida")]
    public DateTime HoraSaida { get; set; }

    [JsonProperty("chegada")]
    public DateTime HoraChegada { get; set; }
    public string Operadora { get; set; }
    public float Preco { get; set; }
  }
}