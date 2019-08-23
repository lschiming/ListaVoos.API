using System;
using Newtonsoft.Json;

namespace ListaVoos.API.Domain.Models
{
  public class Voo
  {
    public int Id { get; set; }

    [JsonProperty("voo")]
    public string CodVoo { get; set; }

    [JsonProperty("origem")]
    public string Origem { get; set; }

    [JsonProperty("destino")]
    public string Destino { get; set; }

    [JsonProperty("data_saida")]
    public DateTime Data { get; set; }

    [JsonProperty("saida")]
    public DateTime HoraSaida { get; set; }

    [JsonProperty("chegada")]
    public DateTime HoraChegada { get; set; }

    [JsonProperty("valor")]
    public float Preco { get; set; }

    [JsonProperty("operadora")]
    public string Operadora { get; set; }
  }
}