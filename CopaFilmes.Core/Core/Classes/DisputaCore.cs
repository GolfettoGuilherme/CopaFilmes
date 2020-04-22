using System;
using CopaFilmes.Core.Interfaces;
using CopaFilmes.Core.Modelos;

namespace CopaFilmes.Core.Classes
{
    internal class DisputaCore : IDisputaCore
    {

        public Filme ExecutarDisputa(Filme filme1, Filme filme2)
        {
            if (filme1.Nota == filme2.Nota)
            {
                if (String.Compare(filme1.Titulo, filme2.Titulo) < 0)
                    return filme1;
                else
                    return filme2;

            }
            else if (filme1.Nota > filme2.Nota)
                return filme1;
            else
                return filme2;
        }
    }
}
