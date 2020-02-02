using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.App.Controller
{
    public class LivrosController
    {
        public string detalhes(int id)
        {
            LivroRepositorioCSV repositorio = new LivroRepositorioCSV();
            var livro = repositorio.Todos.First(r => r.Id == id);
            return livro.Detalhes();

        }

        public IActionResult ParaLer()
        {
            return new ViewResult() { ViewName = "lista" };   
        }

        public static Task Lendo(HttpContext context)
        {
            string html = loadhtmlLivros(new LivroRepositorioCSV().Lendo.Livros);
            return context.Response.WriteAsync(html);
        }
        public static Task Lidos(HttpContext context)
        {
            string html = loadhtmlLivros(new LivroRepositorioCSV().Lidos.Livros);
            return context.Response.WriteAsync(html);
        }
        public string teste()
        {
            return "Sucesso!";
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
