using System;
namespace CopaFilmes.Core.Modelos
{
    public class Filme
    {
        public Filme()
        {
        }

        public string Id { get; set; }
        public string Titulo { get; set; }
        public ushort Ano { get; set; }
        public decimal Nota { get; set; }
    }
}
