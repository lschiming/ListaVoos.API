# ListaVoos.API

REST API, built with ASP.NET Core 2.1, as a coding test for a job interview.
It exposes 2 HTTP requests endpoints:
- A ```/GET``` route (```/aeroportos```), that returns a list of registered airports;
- A ```/POST``` route (```/voos```), that returns a list of flights, including at maximum one extra connection, scheduled according to the parameters passed.

## Frameworks and Libraries
- [ASP.NET Core 2.1](https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-2.1);
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/);
- [Entity Framework In-Memory Provider](https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory);
- [Newtonsoft.Json](https://newtonsoft.com/json);
- [CSVHelper](https://joshclose.github.io/CsvHelper/);
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle).

## How to Test

First, install [.NET Core 2.1](https://dotnet.microsoft.com/download/dotnet-core/2.1).
Afterwards, clone the repository, open the terminal or command prompt at the API root path (```src\ListaVoos.API\```) and run the following commands, in sequence:

```
dotnet restore
dotnet run
```

Using a software such as [Postman](https://getpostman.com/), navigate to ```https://localhost:5001/api/aeroportos``` and execute a HTTP GET request. You should receive a JSON response with all the registered airports.

Alternatively, navigate to ```https://localhost:5001/api/voos``` and execute a HTTP POST request, passing a JSON on the BODY, as exemplified below:

```
{
  "origem": "MCZ",
  "destino": "FLN",
  "data": "2019-02-10"
}
```

The ```origem``` and ```destino``` attribute values can be checked on the ```/api/aeroportos``` response and, for this example, the ```data``` should be between ```2019-02-10``` and ```2019-02-18``` to get you any responses.

_Obs.: If you are getting a "Could not get any response" error, try turning off **SSL certificate verification**._

## Documentation

Navigate to ```https://localhost:5001/swagger``` to check the API documentation.
