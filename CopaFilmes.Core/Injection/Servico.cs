using System;
using CopaFilmes.Core.Modelos;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmes.Core.Injection
{
    public static class Servico
    {
        public static IServiceCollection DI(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddScoped<ICopaFilmesCore, CopaFilmesCore>();

            return services;
            //teste
        }
    }
}
