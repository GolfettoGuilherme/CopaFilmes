using System.Collections.Generic;
using CopaFilmes.Core.Interfaces;
using CopaFilmes.Core.Modelos;

namespace CopaFilmes.Core.Classes
{
    internal class ChaveamentoCore : IChaveamentoCore
    {
        private IDisputaCore _disputaCore;

        public ChaveamentoCore(IDisputaCore disputaCore)
        {
            _disputaCore = disputaCore;
        }

        public List<Filme> Chaveamento(List<Filme> filmes)
        {
            var vencedores = new List<Filme>();
            var totalLista = filmes.Count - 1;
            for (int i = 0; i <= totalLista / 2; i++)
            {
                var filme1 = filmes[i];
                var filme2 = filmes[totalLista - i];

                vencedores.Add(_disputaCore.ExecutarDisputa(filme1, filme2));
            }
            return vencedores;
        }
    }
}
