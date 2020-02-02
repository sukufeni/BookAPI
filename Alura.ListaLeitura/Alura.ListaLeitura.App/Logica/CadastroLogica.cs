using System;
using System.Linq;
using System.Threading.Tasks;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Alura.ListaLeitura.App.Logica
{
    public static class CadastroLogica
    {
        public static Task novoLivro(HttpContext context)
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

        public static Task processaFormulario(HttpContext context)
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

        public static Task exibeFormulario(HttpContext context)
        {
            return context.Response.WriteAsync(HtmlHelper.loadHtml("formulario"));

        }
    }
}
