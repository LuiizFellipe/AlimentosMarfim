using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlimentosMarfim.Models
{
    public class Funcionario
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [Display(Name = "Nome do Funcionário")]
        public string NomeFuncionario { get; set; }


        [Required(ErrorMessage = "PIS é obrigatório")]
        public string PIS { get; set; }


        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Telefone { get; set; }


        [Required(ErrorMessage = "Salário é obrigatório")]
        [Display(Name = "Salário")]
        public double Salario { get; set; }

        // chave estrangeira da model cargo
        [ForeignKey("CargoId")]
        [Display(Name = "Cargo")]
        public int CargoId { get; set; }
        // referenciando a model de cargo
        [Required(ErrorMessage = "Cargo é obrigatório")]
        public Cargo Cargo { get; set; }

        // chave estrangeira da model setor
        [ForeignKey("SetorId")]
        [Display(Name = "Setor")]
        public int SetorId { get; set; }
        // referenciando a model de setor
        [Required(ErrorMessage = "Setor é obrigatório")]
        public Setor Setor { get; set; }

        // chave estrangeira da model de turno
        [ForeignKey("TurnoId")]
        [Display(Name = "Turno")]
        public int TurnoId { get; set; }
        // referenciando a model de turno
        [Required(ErrorMessage = "Turno é obrigatório")]
        public Turno Turno { get; set; }

        // ligação com a model de pedido
        public List<Pedido> Pedidos { get; set; }




    }
}
