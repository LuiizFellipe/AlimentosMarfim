using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlimentosMarfim.Models
{
    public class Turno
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Turno")]
        [Required(ErrorMessage = "Esse campo é obrigatório")]
        public string NomeTurno { get; set; }

        
    }
}
