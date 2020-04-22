using CopaFilmes.Core.Injection;
using CopaFilmes.Core.Interfaces;
using CopaFilmes.Core.Modelos;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace CopaFilmes.Testes
{
    [TestFixture]
    public class Tests
    {
        private IDisputaCore _disputaCore;
        private IChaveamentoCore _chaveamentoCore;
        private IBuscaFilmesCore _buscaFilmesCore;

        private ServiceProvider ServiceProvider { get; set; }

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();

            services.DI();

            ServiceProvider = services.BuildServiceProvider();

        }

        [Test]
        public async System.Threading.Tasks.Task Teste_BuscaApiAsync()
        {
            _buscaFilmesCore = ServiceProvider.GetService<IBuscaFilmesCore>();

            var filmes = await _buscaFilmesCore.Buscar();

            Assert.IsNotNull(filmes);
        }

        [Test]
        public void Teste_RealizaDisputa_NotaMaiorVence()
        {

            _disputaCore = ServiceProvider.GetService<IDisputaCore>();

            var vencedor = new Filme()
            {
                Id = "tt3778644",
                Titulo = "Han Solo: Uma História Star Wars",
                Ano = 2018,
                Nota = 7.2m
            };
            var perdedor = new Filme()
            {
                Id = "tt1365519",
                Titulo = "Tomb Raider: A Origem",
                Ano = 2018,
                Nota = 6.5m
            };

            var retorno = _disputaCore.ExecutarDisputa(vencedor, perdedor);

            Assert.Greater(retorno.Nota, perdedor.Nota);
        }

        [Test]
        public void Teste_RealizaDisputa_Desempate()
        {

            _disputaCore = ServiceProvider.GetService<IDisputaCore>();

            var vencedor = new Filme()
            {
                Id = "tt7784604",
                Titulo = "Hereditário",
                Ano = 2018,
                Nota = 7.8m
            };
            var perdedor = new Filme()
            {
                Id = "tt6499752",
                Titulo = "Upgrade",
                Ano = 2018,
                Nota = 7.8m
            };

            var retorno = _disputaCore.ExecutarDisputa(vencedor, perdedor);

            Assert.AreEqual(retorno, vencedor);
        }

        [Test]
        public async void Teste_RealizaChavemento()
        {

            _chaveamentoCore = ServiceProvider.GetService<IChaveamentoCore>();
            _buscaFilmesCore = ServiceProvider.GetService<IBuscaFilmesCore>();

            var filmes = await _buscaFilmesCore.Buscar();

            var finalistas = _chaveamentoCore.Chaveamento(filmes);

            Assert.Less(finalistas.Count, filmes.Count);
        }

        
    }
}