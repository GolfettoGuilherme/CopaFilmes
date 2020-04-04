using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CopaFilmes.Core.Modelos;

namespace CopaFilmes.Core
{
    public interface ICopaFilmesCore
    {

        Task<List<Filme>> ExecutarCampeonatoAsync(string[] ids);

        List<Filme> Chaveamento(List<Filme> filmes);

        Filme ExecutarDisputa(Filme filme1, Filme filme2);

        Task<List<Filme>> BuscarFilmesApiAsync();
    }
}
