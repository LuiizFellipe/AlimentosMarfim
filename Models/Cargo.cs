using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlimentosMarfim.Models
{
    public class Cargo
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cargo")]
        [Required (ErrorMessage = "Esse campo é obrigatório")]
        public string NomeCargo { get; set; }


        // ligação com a model de funcionário
        public List<Funcionario> Funcionario { get; set; }
    }
}
