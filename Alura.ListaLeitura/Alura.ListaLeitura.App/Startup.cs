using Alura.ListaLeitura.App.Logica;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.ListaLeitura.App
{
    internal class Startup
    {

        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("livros/paraler", ExibicaoLivros.LivrosParaLer);
            builder.MapRoute("livros/lidos", ExibicaoLivros.LivrosLidos);
            builder.MapRoute("livros/lendo", ExibicaoLivros.LivrosLendo);
            builder.MapRoute("livros/detalhes/{id:int}", ExibicaoLivros.getDetalhesLivro);
            RequestDelegateRouteBuilderExtensions.MapRoute(builder, "Cadastro/NovoLivro/{livro}/{autor}", CadastroLogica.novoLivro);
            builder.MapRoute("cadastro/novolivro", CadastroLogica.exibeFormulario);
            builder.MapRoute("cadastro/incluirLivro", CadastroLogica.processaFormulario);

            var routes = builder.Build();
            app.UseRouter(routes);

        }

        public void configureServices(IServiceCollection services)
        {
            services.AddRouting();
        }
    }
}
