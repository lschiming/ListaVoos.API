using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using ListaVoos.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ListaVoos.API.Persistence.Context
{
  public class DataGenerator
  {
    // Arquivo criado para inicializar os dados em memória, padronizando-os
    public static void Initialize(IServiceProvider serviceProvider)
    {
      using (var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
      {
        // Leitura da lista de aeroportos
        // Verificando se já não há dados importados
        if (!context.Aeroportos.Any())
        {
          using (StreamReader file = File.OpenText(@".\Persistence\Context\Files\aeroportos.json"))
          {
            JsonSerializer serializer = new JsonSerializer();
            List<Aeroporto> aeroportos = (List<Aeroporto>)serializer.Deserialize(file, typeof(List<Aeroporto>));
            context.Aeroportos.AddRange(aeroportos);
          }
        }

        // Leitura das listas de vôos
        // Verificando se já não há dados importados
        if (!context.Voos.Any())
        {
          // JSON da 99 Planes
          using (StreamReader file2 = File.OpenText(@".\Persistence\Context\Files\99planes.json"))
          {
            JsonSerializer serializer = new JsonSerializer();
            List<Voo> voos = (List<Voo>)serializer.Deserialize(file2, typeof(List<Voo>));
            foreach (Voo v in voos)
            {
              var date = (v.Data.ToString()).Substring(0, 10);
              var hs = (v.HoraSaida.ToString()).Substring(11, 8);
              var hc = (v.HoraChegada.ToString()).Substring(11, 8);

              DateTime horaSaida = DateTime.Parse(date + ' ' + hs);
              DateTime horaChegada = DateTime.Parse(date + ' ' + hc);

              v.HoraSaida = horaSaida;
              v.HoraChegada = horaChegada;
              v.Operadora = "99 Planes";
            }
            context.Voos.AddRange(voos);
          }

          // CSV da UberAir
          var config = new CsvHelper.Configuration.Configuration
          {
            CultureInfo = CultureInfo.InvariantCulture,
            Delimiter = ",",
            HasHeaderRecord = true,
            MissingFieldFound = null
          };
          using (StreamReader file3 = File.OpenText(@".\Persistence\Context\Files\uberair.csv"))
          using (var csv = new CsvReader(file3, config))
          {
            var records = new List<Voo>();
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
              var date = csv.GetField<string>(3);
              var hs = csv.GetField<string>(4);
              var hc = csv.GetField<string>(5);

              DateTime horaSaida = DateTime.Parse(date + ' ' + hs);
              DateTime horaChegada = DateTime.Parse(date + ' ' + hc);

              var record = new Voo
              {
                CodVoo = csv.GetField<string>(0),
                Origem = csv.GetField<string>(1),
                Destino = csv.GetField<string>(2),
                Data = csv.GetField<DateTime>(3),
                HoraSaida = horaSaida,
                HoraChegada = horaChegada,
                Preco = csv.GetField<float>(6),
                Operadora = "UberAir"
              };
              records.Add(record);
            }
            context.Voos.AddRange(records);
          }
        }

        // Salvando as alterações
        context.SaveChanges();
      }
    }
  }
}