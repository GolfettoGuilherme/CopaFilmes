using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CopaFilmes.Core.Interfaces;
using CopaFilmes.Core.Modelos;
using Newtonsoft.Json;

namespace CopaFilmes.Core.Classes
{
    internal class BuscaFilmesCore : IBuscaFilmesCore
    {
        private readonly IHttpClientFactory _clientFactory;

        private const string URL_API = "http://copafilmes.azurewebsites.net/api/filmes";

        public BuscaFilmesCore(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<Filme>> Buscar()
        {
            try
            {
                using var client = _clientFactory.CreateClient();

                var response = await client.GetAsync(URL_API);

                response.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<List<Filme>>(await response.Content.ReadAsStringAsync());
                return result;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Falha ao buscar filmes, tente novamente");
            }
        }
    }
}
