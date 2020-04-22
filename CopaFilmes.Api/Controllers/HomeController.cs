using System;
using System.Threading.Tasks;
using CopaFilmes.Api.Models;
using CopaFilmes.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CopaFilmes.Api.Controllers
{
    [Route("/api/{controller}")]
    public class HomeController : ControllerBase
    {
        private ICopaFilmesCore _core;
        private IBuscaFilmesCore _buscaFilmesCore;
        private readonly IServiceProvider _provider;

        public HomeController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpPost("ExecutarProcessamento")]
        public async Task<IActionResult> ExecutarProcessamentoAsync([FromBody]ProcessamentoDataRequest entrada)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _core = _provider.GetService<ICopaFilmesCore>();

                    var filmes = await _core.ExecutarCampeonatoAsync(entrada.Ids);

                    return Ok(filmes);
                }
                return BadRequest(new { Mensagem = "Parametros de entrada inválidos, tente novamente" });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpGet("BuscarFilmes")]
        public async Task<IActionResult> BuscarFilmes()
        {
            try
            {
                _buscaFilmesCore = _provider.GetService<IBuscaFilmesCore>();

                var filmes = await _buscaFilmesCore.Buscar();

                return Ok(filmes);
            } catch(Exception)
            {
                return BadRequest();
            }
        }

    }
}
