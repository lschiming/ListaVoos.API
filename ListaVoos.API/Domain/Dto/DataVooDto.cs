using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ListaVoos.API.Domain.Dto
{
  public class DataVooDto
  {
    [JsonProperty("origem")]
    [Required(ErrorMessage = "A origem é necessária.")]
    [StringLength(3, MinimumLength = 3,
      ErrorMessage = "A origem precisa ser uma sigla de 3 dígitos.")]
    public string Origem { get; set; }

    [JsonProperty("destino")]
    [Required(ErrorMessage = "O destino é necessário.")]
    [StringLength(3, MinimumLength = 3,
      ErrorMessage = "O destino precisa ser uma sigla de 3 dígitos.")]
    public string Destino { get; set; }

    [JsonProperty("data")]
    [Required(ErrorMessage = "A data de saída é necessária.")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    public DateTime? Data { get; set; }
  }
}