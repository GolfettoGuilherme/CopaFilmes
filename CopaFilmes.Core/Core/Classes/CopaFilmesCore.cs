using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CopaFilmes.Core.Interfaces;
using CopaFilmes.Core.Modelos;


[assembly: InternalsVisibleTo("CopaFilmes.Testes")]
namespace CopaFilmes.Core.Classes
{
    internal class CopaFilmesCore : ICopaFilmesCore
    {

        private readonly IChaveamentoCore _chaveamentoCore;

        private readonly IBuscaFilmesCore _buscaFilmesCore;

        public CopaFilmesCore(
            IChaveamentoCore chaveamentoCore,
            IBuscaFilmesCore buscaFilmesCore)
        {
            _chaveamentoCore = chaveamentoCore;
            _buscaFilmesCore = buscaFilmesCore;
        }

        public async Task<List<Filme>> ExecutarCampeonatoAsync(string[] ids)
        {
            var retorno = new List<Filme>();

            var filmesCampeonato = (await _buscaFilmesCore.Buscar())
                                    .Where(x => ids.Contains(x.Id))
                                    .OrderBy(y => y.Titulo)
                                    .ToList();


            var vencedoresPrimeiraRodada = _chaveamentoCore.Chaveamento(filmesCampeonato);

            var finalistas = _chaveamentoCore.Chaveamento(vencedoresPrimeiraRodada);

            var vencedor = _chaveamentoCore.Chaveamento(finalistas).FirstOrDefault();

            retorno.Add(vencedor);

            retorno.Add(finalistas.FirstOrDefault(x => x.Id != vencedor.Id));

            return retorno;
        }

    }
}
