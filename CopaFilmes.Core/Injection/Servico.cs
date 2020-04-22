using CopaFilmes.Core.Classes;
using CopaFilmes.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmes.Core.Injection
{
    public static class Servico
    {
        public static IServiceCollection DI(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddScoped<ICopaFilmesCore, CopaFilmesCore>();
            services.AddScoped<IChaveamentoCore, ChaveamentoCore>();
            services.AddScoped<IDisputaCore, DisputaCore>();
            services.AddScoped<IBuscaFilmesCore, BuscaFilmesCore>();

            return services;
        }
    }
}
