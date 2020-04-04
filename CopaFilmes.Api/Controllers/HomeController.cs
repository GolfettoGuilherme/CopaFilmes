﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CopaFilmes.Api.Models;
using CopaFilmes.Core;
using Microsoft.AspNetCore.Mvc;



namespace CopaFilmes.Api.Controllers
{
    [Route("/api/{controller}")]
    public class HomeController : ControllerBase
    {
        private readonly ICopaFilmesCore _core;

        public HomeController(ICopaFilmesCore core)
        {
            this._core = core;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Ok("Oi ;)");
        }

        [HttpPost("ExecutarProcessamento")]
        public async Task<IActionResult> ExecutarProcessamentoAsync([FromBody]ProcessamentoDataRequest entrada)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var filmes = await this._core.ExecutarCampeonatoAsync(entrada.Ids);

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
                var filmes = await this._core.BuscarFilmesApiAsync();

                return Ok(filmes);
            } catch(Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
