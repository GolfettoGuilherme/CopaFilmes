using System;
using System.ComponentModel.DataAnnotations;

namespace CopaFilmes.Api.Models
{
    public class ProcessamentoDataRequest
    {
        [Required]
        public string[] Ids { get; set; }
    }
}

