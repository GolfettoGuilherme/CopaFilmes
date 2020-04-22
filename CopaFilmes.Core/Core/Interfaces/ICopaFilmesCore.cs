using System.Collections.Generic;
using System.Threading.Tasks;
using CopaFilmes.Core.Modelos;

namespace CopaFilmes.Core.Interfaces
{
    public interface ICopaFilmesCore
    {
        Task<List<Filme>> ExecutarCampeonatoAsync(string[] ids);
    }
}
