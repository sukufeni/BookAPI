using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace Alura.ListaLeitura.App.Logica
{
    public static class ExibicaoLivros
    {

        public static Task getDetalhesLivro(HttpContext context)
        {
            LivroRepositorioCSV repositorio = new LivroRepositorioCSV();
            var livro = repositorio.Todos.First(r => r.Id == int.Parse(context.GetRouteValue("id").ToString()));
            return context.Response.WriteAsync(livro.Detalhes());

        }

        public static Task LivrosParaLer(HttpContext context)
        {
            string html = loadhtmlLivros(new LivroRepositorioCSV().ParaLer.Livros);
            return context.Response.WriteAsync(html);
        }

        public static Task LivrosLendo(HttpContext context)
        {
            string html = loadhtmlLivros(new LivroRepositorioCSV().Lendo.Livros);
            return context.Response.WriteAsync(html);
        }
        public static Task LivrosLidos(HttpContext context)
        {
            string html = loadhtmlLivros(new LivroRepositorioCSV().Lidos.Livros);
            return context.Response.WriteAsync(html);
        }

        #region helperMethods

        private static string loadhtmlLivros(IEnumerable<Livro> livros)
        {
            try
            {
                string htmlFile = HtmlHelper.loadHtml("ListaLivro");
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


        #endregion
    }
}
