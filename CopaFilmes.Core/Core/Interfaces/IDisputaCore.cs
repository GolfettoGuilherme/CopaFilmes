using CopaFilmes.Core.Modelos;

namespace CopaFilmes.Core.Interfaces
{
    public interface IDisputaCore
    {
        Filme ExecutarDisputa(Filme filme1, Filme filme2);
    }
}
