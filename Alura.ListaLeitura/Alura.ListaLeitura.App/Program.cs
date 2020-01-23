using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using System;
using Microsoft.AspNetCore.Hosting;

namespace Alura.ListaLeitura.App
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebHost _host = new WebHostBuilder().UseKestrel().UseStartup<Startup>().Build();
            
            _host.Run();

            Console.ReadLine();
        }
    }
}
