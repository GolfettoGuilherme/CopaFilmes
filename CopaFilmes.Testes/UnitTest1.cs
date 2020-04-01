using System.Collections.Generic;
using System.Net.Http;
using CopaFilmes.Core;
using CopaFilmes.Core.Modelos;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

namespace CopaFilmes.Testes
{
    [TestFixture]
    public class Tests
    {
        private ICopaFilmesCore _core;

        [SetUp]
        public void SetUp()
        {
            var factory = new Mock<IHttpClientFactory>();
            Mock<IConfiguration> configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c.GetSection(It.IsAny<string>())).Returns(new Mock<IConfigurationSection>().Object);

            var core = new CopaFilmesCore(factory.Object, configuration.Object);

            this._core = core;
        }

        [Test]
        public void Teste_RealizaDisputa_NotaMaiorVence()
        {
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

            var retorno = _core.ExecutarDisputa(vencedor, perdedor);

            Assert.Greater(retorno.Nota, perdedor.Nota);
        }

        [Test]
        public void Teste_RealizaDisputa_Desempate()
        {
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

            var retorno = _core.ExecutarDisputa(vencedor, perdedor);

            Assert.AreEqual(retorno, vencedor);
        }

        [Test]
        public void Teste_RealizaChavemento()
        {
            var filmes = MontarListaMockada();

            var finalistas = _core.Chaveamento(filmes);

            Assert.Less(finalistas.Count, filmes.Count);
        }

        private List<Filme> MontarListaMockada()
        {
            var retorno = new List<Filme>();
            retorno.Add(new Filme()
            {
                Id = "tt3606756",
                Titulo = "Os Incríveis 2",
                Ano = 2018,
                Nota = 8.5m
            });
            retorno.Add(new Filme()
            {
                Id = "tt4881806",
                Titulo = "Jurassic World: Reino Ameaçado",
                Ano = 2018,
                Nota = 6.7m
            });
            retorno.Add(new Filme()
            {
                Id = "tt5164214",
                Titulo = "Oito Mulheres e um Segredo",
                Ano = 2018,
                Nota = 6.3m
            });
            retorno.Add(new Filme()
            {
                Id = "tt7784604",
                Titulo = "Hereditário",
                Ano = 2018,
                Nota = 7.8m
            });
            retorno.Add(new Filme()
            {
                Id = "tt4154756",
                Titulo = "Vingadores: Guerra Infinita",
                Ano = 2018,
                Nota = 8.8m
            });
            retorno.Add(new Filme()
            {
                Id = "tt5463162",
                Titulo = "Deadpool 2",
                Ano = 2018,
                Nota = 8.1m
            });
            retorno.Add(new Filme()
            {
                Id = "tt3778644",
                Titulo = "Han Solo: Uma História Star Wars",
                Ano = 2018,
                Nota = 7.2m
            });
            retorno.Add(new Filme()
            {
                Id = "tt3501632",
                Titulo = "Thor: Ragnarok",
                Ano = 2017,
                Nota = 7.9m
            });

            return retorno;
        }
    }
}