using System;
using System.Linq;
using System.Threading.Tasks;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.App.Controller
{
    public class CadastroController
    {
        public string novoLivro(Livro livro)
        {
            LivroRepositorioCSV repositorio = new LivroRepositorioCSV();
            try
            {
                repositorio.Incluir(livro);
            }
            catch (Exception e)
            {
                return $"Erro ao incluir Livro! {e.Message}";
            }
            return "livro adicionado com sucesso";
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

        public IActionResult exibeFormulario()
        {
            return new ViewResult() { ViewName = "formulario" };
        }
    }
}
