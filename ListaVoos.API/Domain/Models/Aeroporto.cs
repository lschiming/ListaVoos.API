using Newtonsoft.Json;

namespace ListaVoos.API.Domain.Models
{
  public class Aeroporto
  {
    public int Id { get; set; }

    [JsonProperty("nome")]
    public string Nome { get; set; }

    [JsonProperty("aeroporto")]
    public string Sigla { get; set; }

    [JsonProperty("cidade")]
    public string Cidade { get; set; }
  }
}