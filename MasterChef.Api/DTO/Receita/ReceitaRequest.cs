﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Api.DTO.Receita
{
    public class ReceitaRequest
    {
        [Required(ErrorMessage = "Título deve ser informado")]
        [StringLength(100)]
        public string Titulo { get; set; } = "";

        [Required(ErrorMessage = "Descricao deve ser informada")]
        [StringLength(100)]
        public string Descricao { get; set; } = "";
        
        [Required(ErrorMessage = "Ingrentes deve ser informado")]
        public string Ingredientes { get; set; } = "";

        [Required(ErrorMessage = "Modo de Preparo deve ser informado")]
        public string ModoDePreparo { get; set; } = "";
    }
}