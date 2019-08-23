using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ListaVoos.API.Domain.Repositories;
using ListaVoos.API.Domain.Services;
using ListaVoos.API.Persistence.Context;
using ListaVoos.API.Persistence.Repositories;
using ListaVoos.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace ListaVoos.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase(databaseName: "ListaVoos"));
      services.AddScoped<IAeroportoRepository, AeroportoRepository>();
      services.AddScoped<IVooRepository, VooRepository>();
      services.AddScoped<IAeroportoService, AeroportoService>();
      services.AddScoped<IVooService, VooService>();
      services.AddCors();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info
        {
          Title = "ListaVoos.API",
          Version = "v1",
          Description = "API REST construída como um teste de código para uma entrevista.\n"
            + "Expõe um endpoint para listagem de aeroportos cadastrados e "
            + "um endpoint que permite listar voos de um aeroporto de origem a um de "
            + "destino, com saída em uma data especificada.",
          Contact = new Contact
          {
            Name = "Lucas Schiming",
            Url = "https://github.com/lschiming",
          }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
      });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ListaVoos.API");
        c.DefaultModelsExpandDepth(-1);
      });
      app.UseMvc();
    }
  }
}
