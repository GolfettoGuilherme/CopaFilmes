using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CopaFilmes.Core.Modelos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("CopaFilmes.Testes")]
namespace CopaFilmes.Core
{
    internal class CopaFilmesCore : ICopaFilmesCore
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public CopaFilmesCore(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this._clientFactory = clientFactory;
            this._configuration = configuration;
        }

        private async Task<List<Filme>> BuscarFilmesApiAsync()
        {
            try
            {
                using (var client = this._clientFactory.CreateClient())
                {
                    var url = _configuration.GetValue<string>("UrlApi");

                    var response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    var result = JsonConvert.DeserializeObject<List<Filme>>(await response.Content.ReadAsStringAsync());
                    return result;
                }
            }
            catch(HttpRequestException)
            {
                throw new Exception("Falha ao buscar filmes, tente novamente");
            }   
        }

        public async Task<List<Filme>> ExecutarCampeonatoAsync(string[] ids)
        {
            var retorno = new List<Filme>();

            var filmesCampeonato = (await BuscarFilmesApiAsync())
                                    .Where(x => ids.Contains(x.Id))
                                    .OrderBy(y => y.Titulo)
                                    .ToList();


            var vencedoresPrimeiraRodada = Chaveamento(filmesCampeonato);


            var finalistas = Chaveamento(vencedoresPrimeiraRodada);


            var vencedor = Chaveamento(finalistas).FirstOrDefault();

            retorno.Add(vencedor);

            retorno.Add(finalistas.FirstOrDefault(x => x.Id != vencedor.Id));


            return retorno;
        }

        public List<Filme> Chaveamento(List<Filme> filmes)
        {
            var vencedores = new List<Filme>();
            var totalLista = filmes.Count - 1;
            for (int i = 0; i <= totalLista / 2; i++)
            {
                var filme1 = filmes[i];
                var filme2 = filmes[totalLista - i];

                vencedores.Add(ExecutarDisputa(filme1, filme2));
            }
            return vencedores;
        }

        public Filme ExecutarDisputa(Filme filme1, Filme filme2)
        {
            if(filme1.Nota == filme2.Nota)
            {
                if (String.Compare(filme1.Titulo, filme2.Titulo) < 0)
                    return filme1;
                else
                    return filme2;

            } else if(filme1.Nota > filme2.Nota)
                return filme1;
            else
                return filme2;
            

        }
    }
}
