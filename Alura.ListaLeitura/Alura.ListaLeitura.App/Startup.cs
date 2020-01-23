using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.ListaLeitura.App
{
    internal class Startup
    {

        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("livros/paraler", this.LivrosParaLer);
            builder.MapRoute("livros/lidos", LivrosLidos);
            builder.MapRoute("livros/lendo", LivrosLendo);
            builder.MapRoute("livros/detalhes/{id:int}", getDetalhesLivro);
            builder.MapRoute("Cadastro/NovoLivro/{livro}/{autor}", novoLivro);

            var routes = builder.Build();
            app.UseRouter(routes);

        }

        private Task getDetalhesLivro(HttpContext context)
        {
            LivroRepositorioCSV repositorio = new LivroRepositorioCSV();
            var livro = repositorio.Todos.First(r => r.Id == int.Parse(context.GetRouteValue("id").ToString()));
            return context.Response.WriteAsync(livro.Detalhes());

        }

        private Task novoLivro(HttpContext context)
        {
            LivroRepositorioCSV repositorio = new LivroRepositorioCSV();
            try
            {
                Livro livro = new Livro()
                {
                    Titulo = context.GetRouteValue("livro").ToString(),
                    Autor = context.GetRouteValue("autor").ToString()
                };

                repositorio.Incluir(livro);
            }
            catch (Exception e)
            {
                return context.Response.WriteAsync($"Erro ao incluir Livro! {e.Message}");
            }
            return context.Response.WriteAsync("livro adicionado com sucesso");
        }

        public Task LivrosParaLer(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(repo.ParaLer.ToString());
        }
        public Task LivrosLendo(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(repo.Lendo.ToString());
        }
        public Task LivrosLidos(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(repo.Lidos.ToString());
        }

        public Task RoutingTask(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();

            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("Caminho inexistente");
        }

        public void configureServices(IServiceCollection services)
        {
            services.AddRouting();
        }
    }
}
