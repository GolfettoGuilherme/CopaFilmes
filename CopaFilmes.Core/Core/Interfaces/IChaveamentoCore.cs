using System.Collections.Generic;
using CopaFilmes.Core.Modelos;

namespace CopaFilmes.Core.Interfaces
{
    public interface IChaveamentoCore
    {
        List<Filme> Chaveamento(List<Filme> filmes);
    }
}
