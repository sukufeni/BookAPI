using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    internal class Startup
    {

        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("livros/paraler", LivrosParaLer);
            builder.MapRoute("livros/lidos", LivrosLidos);
            builder.MapRoute("livros/lendo", LivrosLendo);
            builder.MapRoute("livros/detalhes/{id:int}", getDetalhesLivro);
            builder.MapRoute("Cadastro/NovoLivro/{livro}/{autor}", novoLivro);
            builder.MapRoute("cadastro/novolivro", exibeFormulario);
            builder.MapRoute("cadastro/incluirLivro", processaFormulario);

            var routes = builder.Build();
            app.UseRouter(routes);

        }

        private Task processaFormulario(HttpContext context)
        {
            LivroRepositorioCSV repositorioCsv = new LivroRepositorioCSV();
            Livro livro = new Livro()
            {
                Titulo = context.Request.Form["titulo"].First(),
                Autor = context.Request.Form["autor"].First()
            };
            repositorioCsv.Incluir(livro);

            return context.Response.WriteAsync("Livro incluido com sucesso!");
        }

        private string loadHtml(string form)
        {
            string htmlPath = $@"C:\Users\Bruno Vieira\Google Drive\programacao\C#\git\BookAPI\Alura.ListaLeitura\Alura.ListaLeitura.App\HTML\{form}.html";

            try
            {
                using (var arquivo = File.OpenText(htmlPath))
                {
                    return arquivo.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        private Task exibeFormulario(HttpContext context)
        {
            return context.Response.WriteAsync(loadHtml("formulario"));

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
            string html = loadhtmlLivros(new LivroRepositorioCSV().ParaLer.Livros);
            return context.Response.WriteAsync(html);
        }

        public Task LivrosLendo(HttpContext context)
        {
            string html = loadhtmlLivros(new LivroRepositorioCSV().Lendo.Livros);
            return context.Response.WriteAsync(html);
        }
        public Task LivrosLidos(HttpContext context)
        {
            string html = loadhtmlLivros(new LivroRepositorioCSV().Lidos.Livros);
            return context.Response.WriteAsync(html);
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

        private string loadhtmlLivros(IEnumerable<Livro> livros)
        {
            try
            {
                string htmlFile = loadHtml("ListaLivro");
                foreach (Livro livro in livros)
                {
                    htmlFile = htmlFile.Replace("#novolivro", $"<li>{livro.Titulo} - {livro.Autor}</li> #novolivro");
                }
                return htmlFile.Replace("#novolivro", "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
